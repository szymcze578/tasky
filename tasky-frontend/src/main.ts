import './assets/main.css'
import './config/dayjs.ts'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { useUserStore } from './stores/userStore.ts'
import { useBoardStore } from './stores/boardStore.ts'

const app = createApp(App)
app.use(createPinia())

async function bootstrap() {
  const userStore = useUserStore()
  const boardStore = useBoardStore()

  try {
    await userStore.fetchUser()
    if (userStore.user) {
      await boardStore.fetchBoards(userStore.user.userId)
    }
  } catch {
    console.warn('Failed to initialize app data')
  }

  app.use(router)
  app.mount('#app')
}

bootstrap()
