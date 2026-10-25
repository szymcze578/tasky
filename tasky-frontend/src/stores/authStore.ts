import { authApi } from '@/api/auth'
import type { LoginUserRequest } from '@/api/types'
import router from '@/router'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

const ACCESS_TOKEN_KEY = 'accessToken'
const EXPIRES_AT_KEY = 'tokenExpiresAt'
const EXPIRY_MARGIN_MS = 5_000 // token wygasa co do sekundy (ClockSkew = 0), więc wylogowujemy chwilę wcześniej
const MAX_TIMEOUT_MS = 2_147_483_647 // limit setTimeout (~24,8 dnia)

export const useAuthStore = defineStore('auth', () => {
  const accessToken = ref(localStorage.getItem(ACCESS_TOKEN_KEY) ?? '')
  const expiresAt = ref(localStorage.getItem(EXPIRES_AT_KEY) ?? '')
  let expiryTimer: ReturnType<typeof setTimeout> | undefined

  const isAuthenticated = computed(() => accessToken.value !== '')

  function msUntilExpiry() {
    return new Date(expiresAt.value).getTime() - Date.now() - EXPIRY_MARGIN_MS
  }

  function clearSession() {
    clearTimeout(expiryTimer)
    accessToken.value = ''
    expiresAt.value = ''
    localStorage.removeItem(ACCESS_TOKEN_KEY)
    localStorage.removeItem(EXPIRES_AT_KEY)
  }

  function scheduleExpiry() {
    clearTimeout(expiryTimer)
    if (!accessToken.value || !expiresAt.value) return

    const msLeft = msUntilExpiry()
    if (msLeft <= 0) {
      logout()
      return
    }
    // przy bardzo długim czasie życia timer odpala się wcześniej i planuje się ponownie
    expiryTimer = setTimeout(scheduleExpiry, Math.min(msLeft, MAX_TIMEOUT_MS))
  }

  async function login(payload: LoginUserRequest) {
    const result = await authApi.login(payload)

    accessToken.value = result.accessToken ?? ''
    expiresAt.value = result.expiresAt ?? ''
    localStorage.setItem(ACCESS_TOKEN_KEY, accessToken.value)
    localStorage.setItem(EXPIRES_AT_KEY, expiresAt.value)
    scheduleExpiry()
  }

  function logout() {
    clearSession()
    router.push('/login')
  }

  if (accessToken.value && (!expiresAt.value || msUntilExpiry() <= 0)) {
    clearSession()
  } else {
    scheduleExpiry()
  }

  return { accessToken, expiresAt, isAuthenticated, login, logout }
})
