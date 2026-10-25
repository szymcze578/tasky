// @vitest-environment node
import { describe, it, expect, vi, beforeEach } from 'vitest'

const auth = vi.hoisted(() => ({ accessToken: null as string | null, logout: vi.fn() }))
vi.mock('@/stores/authStore', () => ({ useAuthStore: () => auth }))

const json = (status: number, body: unknown = {}) =>
  new Response(JSON.stringify(body), { status, headers: { 'Content-Type': 'application/json' } })

describe('api client middleware', () => {
  const fetchMock = vi.fn()

  async function loadClient() {
    vi.resetModules()
    vi.stubGlobal('fetch', fetchMock)
    return (await import('../client')).api
  }

  beforeEach(() => {
    fetchMock.mockReset()
    auth.accessToken = null
    auth.logout.mockClear()
  })

  it('adds the Bearer token when the user is logged in', async () => {
    auth.accessToken = 'jwt-token'
    fetchMock.mockImplementation(async () => json(200, []))
    const api = await loadClient()

    await api.GET('/api/boards')

    const request = fetchMock.mock.calls[0]?.[0] as Request
    expect(request.headers.get('Authorization')).toBe('Bearer jwt-token')
  })

  it('does not send Authorization without a token', async () => {
    fetchMock.mockImplementation(async () => json(200, []))
    const api = await loadClient()

    await api.GET('/api/boards')

    const request = fetchMock.mock.calls[0]?.[0] as Request
    expect(request.headers.has('Authorization')).toBe(false)
  })

  it('logs out on 401 from a protected endpoint', async () => {
    auth.accessToken = 'expired'
    fetchMock.mockImplementation(async () => json(401))
    const api = await loadClient()

    await api.GET('/api/boards')

    expect(auth.logout).toHaveBeenCalledOnce()
  })

  it('does not log out on 401 from an auth endpoint (wrong password)', async () => {
    auth.accessToken = 'stale-token'
    fetchMock.mockImplementation(async () => json(401, { detail: 'Invalid credentials' }))
    const api = await loadClient()

    await api.POST('/api/auth/login', { body: { login: 'a@b.pl', password: 'x' } })

    expect(auth.logout).not.toHaveBeenCalled()
  })

  it('does not log out on 401 when the request had no token', async () => {
    fetchMock.mockImplementation(async () => json(401))
    const api = await loadClient()

    await api.GET('/api/boards')

    expect(auth.logout).not.toHaveBeenCalled()
  })

  it('does not log out on other error statuses', async () => {
    auth.accessToken = 'jwt-token'
    fetchMock.mockImplementation(async () => json(404, { detail: 'Not found' }))
    const api = await loadClient()

    await api.GET('/api/boards')

    expect(auth.logout).not.toHaveBeenCalled()
  })
})
