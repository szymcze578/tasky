import { useAuthStore } from '@/stores/authStore'
import createClient, { type Middleware } from 'openapi-fetch'
import type { paths } from './schema'

export interface ApiError {
  status: number
  message: string
}

export const api = createClient<paths>({
  baseUrl: import.meta.env.VITE_API_URL ?? 'http://localhost:6060',
})

const authMiddleware: Middleware = {
  onRequest({ request }) {
    const { accessToken } = useAuthStore()
    if (accessToken) request.headers.set('Authorization', `Bearer ${accessToken}`)
    return request
  },

  onResponse({ request, response }) {
    // 401 z endpointu auth (zły login) nie oznacza wygasłej sesji
    const isAuthCall = new URL(request.url).pathname.startsWith('/api/auth')
    if (response.status === 401 && request.headers.has('Authorization') && !isAuthCall) {
      useAuthStore().logout()
    }
  },
}

api.use(authMiddleware)
