import { user as fetchCurrentUser } from '@/api/user'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { User } from '@/types/types'

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
  const user = ref<User | null>(null)

  async function fetchUser() {
    const result = await fetchCurrentUser()
    user.value = {
      name: result.userName ?? '',
      plan: result.plan ?? '',
      initials: initialsFrom(result.userName),
      userId: result.userId ?? '',
    }
    return user.value
  }

  return { user, fetchUser }
})
