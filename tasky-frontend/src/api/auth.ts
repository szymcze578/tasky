import { apiClient } from './client'
import type { components } from './schema'

export type CreateUserRequest = components['schemas']['CreateUserRequest']
export type LoginUserRequest = components['schemas']['LoginUserRequest']
export type AuthResponse = components['schemas']['AuthResponse']

export async function registerUser(payload: CreateUserRequest): Promise<unknown> {
  const { data } = await apiClient.post<CreateUserRequest>('/api/auth/register', payload)
  return data
}

export async function login(payload: LoginUserRequest): Promise<AuthResponse> {
  const { data } = await apiClient.post<AuthResponse>('/api/auth/login', payload)
  return data
}
