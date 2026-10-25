import { api } from './client'
import { unwrap } from './errors'
import type { ChangeTaskStatusRequest, CreateTaskRequest, Task, UpdateTaskRequest } from './types'

function getTasks(boardId: string): Promise<Task[]> {
  return unwrap(api.GET('/api/boards/{boardId}/tasks', { params: { path: { boardId } } }))
}

function getTaskBy(boardId: string, taskId: string): Promise<Task> {
  return unwrap(
    api.GET('/api/boards/{boardId}/tasks/{taskId}', { params: { path: { boardId, taskId } } }),
  )
}

function addTask(boardId: string, task: CreateTaskRequest): Promise<Task> {
  return unwrap(
    api.POST('/api/boards/{boardId}/tasks', { params: { path: { boardId } }, body: task }),
  )
}

function updateTask(boardId: string, taskId: string, request: UpdateTaskRequest): Promise<Task> {
  return unwrap(
    api.PUT('/api/boards/{boardId}/tasks/{taskId}', {
      params: { path: { boardId, taskId } },
      body: request,
    }),
  )
}

function changeTaskStatus(
  boardId: string,
  taskId: string,
  request: ChangeTaskStatusRequest,
): Promise<Task> {
  return unwrap(
    api.PUT('/api/boards/{boardId}/tasks/{taskId}/status', {
      params: { path: { boardId, taskId } },
      body: request,
    }),
  )
}

function deleteTask(boardId: string, taskId: string) {
  return unwrap(
    api.DELETE('/api/boards/{boardId}/tasks/{taskId}', {
      params: { path: { boardId, taskId } },
    }),
  )
}

export const tasksApi = {
  getTasks,
  getTaskBy,
  addTask,
  updateTask,
  changeTaskStatus,
  deleteTask,
}
