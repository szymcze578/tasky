import { userApi } from '@/api/user'
import type { UserDto } from '@/api/types'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

function initialsFrom(userName: string | null | undefined): string {
  if (!userName) return ''
  return userName
    .trim()
    .split(/\s+/)
    .map((part) => part[0])
    .join('')
    .slice(0, 2)
    .toUpperCase()
}

export const useUserStore = defineStore('user', () => {
  const user = ref<UserDto | null>(null)
  const initials = computed(() => initialsFrom(user.value?.userName))

  async function fetchUser() {
    user.value = await userApi.getMe()
    return user.value
  }

  function reset() {
    user.value = null
  }

  return { user, initials, fetchUser, reset }
})
