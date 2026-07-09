import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { User } from '@/types/types'

export const useUserStore = defineStore('user', () => {
  const user = ref<User | null>(null)

  function fetchUser() {
    user.value = {
      name: 'Szymon Czech',
      plan: 'Premium plan',
      initials: 'SC',
      userId: '11111111-1111-1111-1111-111111111111',
    }
    return user.value
  }

  return { user, fetchUser }
})
