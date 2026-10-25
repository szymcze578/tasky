// src/api/auth.ts
import { api } from './client'
import { unwrap } from './errors'
import type { AuthResponse, CreateUserRequest, LoginUserRequest, RegisterResponse } from './types'

function registerUser(body: CreateUserRequest): Promise<RegisterResponse> {
  return unwrap(api.POST('/api/auth/register', { body }))
}

function login(body: LoginUserRequest): Promise<AuthResponse> {
  return unwrap(api.POST('/api/auth/login', { body }))
}

export const authApi = {
  register: registerUser,
  login,
}
