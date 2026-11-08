import { bootstrap } from '@/api/services/bootstrap'
import AppLayout from '@/layouts/AppLayout.vue'
import AuthLayout from '@/layouts/AuthLayout.vue'
import { useAuthStore } from '@/stores/authStore'
import { useUserStore } from '@/stores/userStore'
import BoardListScreen from '@/views/BoardListScreen.vue'
import ListView from '@/views/ListView.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: AppLayout,
      children: [
        { path: '', name: 'boardList', component: BoardListScreen },
        { path: 'boards/:boardId', name: 'board', component: ListView, props: true },
      ],
    },
    {
      path: '/',
      component: AuthLayout,
      meta: { public: true },
      children: [
        { path: 'login', name: 'login', component: () => import('@/views/LoginView.vue') },
        { path: 'register', name: 'register', component: () => import('@/views/RegisterView.vue') },
      ],
    },
    {
      path: '/address',
      component: AuthLayout,
      children: [
        {
          path: '',
          name: 'address',
          component: () => import('@/views/AddressView.vue'),
        },
      ],
    },
  ],
})

router.beforeEach(async (to) => {
  const auth = useAuthStore()

  if (to.meta.public) {
    return auth.isAuthenticated && to.name === 'login' ? { name: 'boardList' } : true
  }

  if (!auth.isAuthenticated) return { name: 'login' }
  if (!(await bootstrap())) return { name: 'login' }

  return true
})

export default router
