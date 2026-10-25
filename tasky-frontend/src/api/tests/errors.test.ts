import { describe, it, expect } from 'vitest'
import { ApiError, isApiError, unwrap, unwrapVoid } from '../errors'

const ok = <T>(data: T, status = 200) => ({ data, response: new Response(null, { status }) })
const failed = (status: number, error: unknown) => ({
  error,
  response: new Response(null, { status }),
})

describe('ApiError.fromResponse', () => {
  it('uses detail from ProblemDetails as the message', () => {
    const error = ApiError.fromResponse(404, { title: 'Not Found', detail: 'Board not found' })

    expect(error).toBeInstanceOf(Error)
    expect(error.status).toBe(404)
    expect(error.message).toBe('Board not found')
  })

  it('falls back to title when detail is missing', () => {
    expect(ApiError.fromResponse(500, { title: 'Unexpected error' }).message).toBe(
      'Unexpected error',
    )
  })

  it('falls back to a generic message for an empty or non-JSON body', () => {
    expect(ApiError.fromResponse(401, '').message).toBe('Request failed (401)')
    expect(ApiError.fromResponse(502, undefined).message).toBe('Request failed (502)')
  })

  it('lower-cases the first letter of validation error keys', () => {
    const error = ApiError.fromResponse(400, {
      title: 'One or more validation errors occurred.',
      errors: { Name: ['Name is required'], 'Address.City': ['Too long'] },
    })

    expect(error.fieldErrors).toEqual({ name: ['Name is required'], 'address.City': ['Too long'] })
  })

  it('has no fieldErrors when the body has none', () => {
    expect(ApiError.fromResponse(404, { detail: 'x' }).fieldErrors).toBeUndefined()
  })
})

describe('isApiError', () => {
  it('recognises ApiError instances only', () => {
    expect(isApiError(new ApiError(400, 'x'))).toBe(true)
    expect(isApiError(new Error('x'))).toBe(false)
    expect(isApiError({ status: 400, message: 'x' })).toBe(false)
  })
})

describe('unwrap', () => {
  it('returns data for a successful response', async () => {
    await expect(unwrap(Promise.resolve(ok({ id: '1' })))).resolves.toEqual({ id: '1' })
  })

  it('throws ApiError with status and detail for a failed response', async () => {
    const promise = unwrap(Promise.resolve(failed(404, { detail: 'Board not found' })))

    await expect(promise).rejects.toMatchObject({
      name: 'ApiError',
      status: 404,
      message: 'Board not found',
    })
  })

  it('maps a network failure to ApiError with status 0', async () => {
    const promise = unwrap(Promise.reject(new TypeError('Failed to fetch')))

    await expect(promise).rejects.toMatchObject({ name: 'ApiError', status: 0 })
  })

  it('rethrows AbortError unchanged', async () => {
    const abort = new DOMException('Aborted', 'AbortError')

    await expect(unwrap(Promise.reject(abort))).rejects.toBe(abort)
  })
})

describe('unwrapVoid', () => {
  it('resolves for a 204 response', async () => {
    await expect(unwrapVoid(Promise.resolve(ok(undefined, 204)))).resolves.toBeUndefined()
  })

  it('throws ApiError for a failed response', async () => {
    await expect(
      unwrapVoid(Promise.resolve(failed(409, { detail: 'Conflict' }))),
    ).rejects.toMatchObject({
      status: 409,
      message: 'Conflict',
    })
  })
})
