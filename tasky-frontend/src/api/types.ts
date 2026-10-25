import type { components } from './schema'

type Schemas = components['schemas']

export type Board = Schemas['BoardDto']
export type BoardType = Schemas['BoardType'] // 'List' | 'Kanban'
export type CreateBoardRequest = Schemas['CreateBoardRequest']
export type UpdateBoardRequest = Schemas['UpdateBoardRequest']

export type Task = Schemas['TaskDto']
export type CreateTaskRequest = Schemas['CreateTaskRequest']
export type ChangeTaskStatusRequest = Schemas['ChangeTaskStatusRequest']
export type UpdateTaskRequest = Schemas['UpdateTaskRequest']

export type CreateUserRequest = Schemas['CreateUserRequest']
export type LoginUserRequest = Schemas['LoginUserRequest']
export type AuthResponse = Schemas['AuthResponse']
export type RegisterResponse = Schemas['RegisterResponse']

export type UserDto = Schemas['UserDto']
export type UpdateUserRequest = Schemas['UpdateUserRequest']
export type AddressRequest = Schemas['AddressRequest']
