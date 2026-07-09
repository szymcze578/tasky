import { apiClient } from './client'
import type { components } from './schema'

export type CreateUserViewModel = components['schemas']['CreateUserViewModel']

// Backend controllers don't declare response types (no [ProducesResponseType]),
// so Swagger/schema.d.ts has no schema for these responses — typed as unknown
// until the backend annotates them.

export function getUsers() {
  return apiClient.get<unknown>('/api/user')
}

export function createUser(payload: CreateUserViewModel) {
  return apiClient.post<unknown>('/api/user', payload)
}
