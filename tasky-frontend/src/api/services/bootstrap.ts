import { useAuthStore } from '@/stores/authStore'
import { useBoardStore } from '@/stores/boardStore'
import { useUserStore } from '@/stores/userStore'

export async function bootstrap() {
  const authStore = useAuthStore()
  const userStore = useUserStore()
  const boardStore = useBoardStore()

  if (!authStore.isAuthenticated) {
    return
  }

  try {
    await userStore.fetchUser()
    if (userStore.user) {
      await boardStore.fetchBoards(userStore.user.userId)
    }
  } catch {
    authStore.logout()
  }
}
