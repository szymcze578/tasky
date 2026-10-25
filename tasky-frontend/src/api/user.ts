import { api } from './client'
import { unwrap, unwrapVoid } from './errors'
import type { AddressRequest, UpdateUserRequest, UserDto } from './types'

function getMe(): Promise<UserDto> {
  return unwrap(api.GET('/api/users/me'))
}

function updateMe(body: UpdateUserRequest): Promise<UserDto> {
  return unwrap(api.PUT('/api/users/me', { body }))
}

function setAddress(body: AddressRequest): Promise<UserDto> {
  return unwrap(api.PUT('/api/users/me/address', { body }))
}

function removeAddress() {
  return unwrapVoid(api.DELETE('/api/users/me/address'))
}

export const userApi = {
  getMe,
  update: updateMe,
  setAddress,
  removeAddress,
}
