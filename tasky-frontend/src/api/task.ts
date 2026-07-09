import { apiClient } from './client'

// Backend controllers don't declare response types (no [ProducesResponseType]),
// so Swagger/schema.d.ts has no schema for these responses — typed as unknown
// until the backend annotates them.

export function getTasks(boardId: string) {
  return apiClient.get<unknown>('/api/task', { params: { boardId } })
}

export function addTask(boardId: string) {
  return apiClient.post<unknown>('/api/task', null, { params: { boardId } })
}
