import axios, { AxiosError } from 'axios'

export interface ApiError {
  status: number
  message: string
}

export function normalizeError(error: unknown): ApiError {
  if (error instanceof AxiosError) {
    return {
      status: error.response?.status ?? 0,
      message: error.response?.data?.message ?? error.message,
    }
  }

  return { status: 0, message: 'Unknown error' }
}

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' },
})

apiClient.interceptors.response.use(
  (response) => response,
  (error: unknown) => Promise.reject(normalizeError(error)),
)
