import { login as loginRequest } from '@/api/auth'
import type { LoginUserRequest } from '@/api/auth'
import router from '@/router'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

const ACCESS_TOKEN_KEY = 'accessToken'
const EXPIRATION_KEY = 'tokenExpiration'

export const useAuthStore = defineStore('auth', () => {
  const accessToken = ref(localStorage.getItem(ACCESS_TOKEN_KEY) ?? '')
  const expirationTime = ref(localStorage.getItem(EXPIRATION_KEY) ?? '')

  const isAuthenticated = computed(() => accessToken.value !== '')

  async function login(payload: LoginUserRequest) {
    const result = await loginRequest(payload)
    accessToken.value = result.accessToken ?? ''
    expirationTime.value = result.expiration ?? ''
    localStorage.setItem(ACCESS_TOKEN_KEY, accessToken.value)
    localStorage.setItem(EXPIRATION_KEY, expirationTime.value)
  }

  function logout() {
    accessToken.value = ''
    expirationTime.value = ''
    localStorage.removeItem(ACCESS_TOKEN_KEY)
    localStorage.removeItem(EXPIRATION_KEY)
    router.push('/login')
  }

  return { accessToken, expirationTime, isAuthenticated, login, logout }
})
