import { useUserStore } from '@/stores/userStore'
import BoardListScreen from '@/views/BoardListScreen.vue'
import BoardView from '@/views/BoardView.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: BoardView,
      children: [
        // Add future board routes here, e.g.:
        { path: '', name: 'boardList', component: BoardListScreen },
        // { path: ':boardId', name: 'board', component: BoardScreen },
      ],
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('@/views/RegisterView.vue'),
    },
  ],
})

router.beforeEach((to) => {
  const userStore = useUserStore()
  if (!userStore.user && to.name !== 'login') {
    return { name: 'login' }
  }
})

export default router
