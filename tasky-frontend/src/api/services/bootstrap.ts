import { useAuthStore } from '@/stores/authStore'
import { useBoardStore } from '@/stores/boardStore'
import { useUserStore } from '@/stores/userStore'
import { ref } from 'vue'

let pending: Promise<boolean> | null = null

export const isBootstrapping = ref(false)
const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms))

export function bootstrap(): Promise<boolean> {
  const authStore = useAuthStore()
  const userStore = useUserStore()
  const boardStore = useBoardStore()

  if (!authStore.isAuthenticated) return Promise.resolve(false)
  if (userStore.user) return Promise.resolve(true)

  isBootstrapping.value = true
  pending ??= Promise.all([userStore.fetchUser(), boardStore.fetchBoards()])
    .then(() => true)
    .catch(() => {
      authStore.logout()
      return false
    })
    .finally(() => {
      pending = null
      isBootstrapping.value = false
    })

  return pending
}
