<template>
  <AuthCard title="Welcome back">
    <template #subtitle>Log in to pick up where you left off.</template>
    <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
      <AuthTextField v-model="email" label="Email" type="email" autocomplete="email" :icon="Mail" />
      <AuthTextField
        v-model="password"
        label="Password"
        type="password"
        autocomplete="current-password"
        :icon="Lock"
      />
      <!-- <AuthCheckbox v-model="remember">Keep me signed in</AuthCheckbox>-->
      <button
        type="submit"
        :disabled="!isValid"
        class="mt-2 rounded-xl bg-ink px-3 py-2.5 text-[14.5px] font-medium text-cream-50 transition-colors disabled:cursor-not-allowed disabled:opacity-50"
      >
        Log in
      </button>

      <div class="flex items-center gap-3.5">
        <div class="h-px flex-1 bg-line" />
        <span class="text-[12px] font-semibold text-ink-300">or</span>
        <div class="h-px flex-1 bg-line" />
      </div>
      <button
        type="button"
        class="flex w-full items-center justify-center gap-2.75 rounded-xl border border-line-soft bg-white p-3 py-2.5 text-[14.5px] font-bold text-ink-500 transition-colors hover:bg-cream-300"
        @click="handleGoogle"
      >
        <img src="/public/GoogleIcon.png" width="25" height="25" />
        <span>Continue with Google</span>
      </button>
    </form>

    <template #footer>
      New to Tasky?
      <a href="/register" class="font-bold text-coral-600 no-underline">Create an account</a>
    </template>
  </AuthCard>
</template>

<script setup lang="ts">
  import { computed, ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { Mail, Lock } from '@lucide/vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'
  import AuthCard from '@/components/auth/AuthCard.vue'
  import { useAuthStore } from '@/stores/authStore'
  import { bootstrap } from '@/api/services/bootstrap'
  import type { ApiError } from '@/api/client'

  const router = useRouter()

  const email = ref('')
  const password = ref('')
  const errorMessage = ref('')

  const isValid = computed(() => email.value.trim() !== '' && password.value.trim() !== '')

  async function handleSubmit() {
    if (!isValid.value) {
      return
    }

    try {
      await useAuthStore().login({ login: email.value, password: password.value })
      await bootstrap()
      router.push('/')
    } catch (err) {
      const { status, message } = err as ApiError
      errorMessage.value = status === 401 ? 'Invalid email or password.' : message
      console.error(errorMessage.value)
    }
  }

  function handleGoogle() {
    console.log('Login in with Google')
  }
</script>
