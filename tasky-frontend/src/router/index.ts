import { useAuthStore } from '@/stores/authStore'
import { useUserStore } from '@/stores/userStore'
import BoardListScreen from '@/views/BoardListScreen.vue'
import BoardView from '@/views/BoardView.vue'
import ListView from '@/views/ListView.vue'
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
        { path: '/boardId', name: 'board', component: ListView },
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
      children: [
        { path: '/address', name: 'address', component: () => import('@/views/AddressView.vue') },
      ],
    },
  ],
})

router.beforeEach((to) => {
  if (!useAuthStore().isAuthenticated && to.name !== 'login' && to.name !== 'register') {
    return { name: 'login' }
  }
  const userStore = useUserStore()
  // if (!userStore.user && to.name !== 'login' && to.name !== 'register') {
  //   return { name: 'login' }
  // }
})

export default router
