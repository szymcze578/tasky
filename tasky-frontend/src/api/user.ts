import { apiClient } from './client'
import type { components } from './schema'

export type CreateUserRequest = components['schemas']['CreateUserRequest']
export type User = components['schemas']['UserViewModel']

// Backend controllers don't declare response types (no [ProducesResponseType]),
// so Swagger/schema.d.ts has no schema for these responses — typed as unknown
// until the backend annotates them.

export function getUsers() {
  return apiClient.get<unknown>('/api/user')
}

export function createUser(payload: CreateUserRequest) {
  return apiClient.post<unknown>('/api/user', payload)
}

export async function user(): Promise<User> {
  const { data } = await apiClient.get<User>('api/user/me')
  return data
}
