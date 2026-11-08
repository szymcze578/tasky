<template>
  <AuthCard class="min-w-105">
    <template #title>Welcome back</template>
    <template #subtitle>Log in to pick up where you left off.</template>
    <template #content>
      <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
        <AuthTextField
          v-model="email"
          label="Email"
          type="email"
          autocomplete="email"
          :icon="Mail"
        />
        <AuthPasswordField
          v-model="password"
          label="Password"
          placeholder="••••••••"
          autocomplete="current-password"
        />
        <AuthCheckbox v-model="remember">
          <span class="text-[13.5px] font-semibold leading-[1.4] text-ink-600"
            >Keep me signed in
          </span>
        </AuthCheckbox>
        <p v-if="errorMessage" role="alert" class="text-[13px] text-danger">{{ errorMessage }}</p>

        <AuthSubmitButton :disabled="!canSubmit">Log in</AuthSubmitButton>
        <AuthDivider />
        <GoogleButton />
      </form>
    </template>
    <template #footer>
      New to Tasky?
      <RouterLink to="/register" class="font-bold text-coral-600 no-underline">
        Create an account
      </RouterLink>
    </template>
  </AuthCard>
</template>

<script setup lang="ts">
  import { computed, ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { Mail } from '@lucide/vue'
  import { errorMessageFrom, isApiError } from '@/api/errors'
  import AuthCard from '@/components/auth/AuthCard.vue'
  import AuthDivider from '@/components/auth/AuthDivider.vue'
  import AuthSubmitButton from '@/components/auth/AuthSubmitButton.vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'
  import AuthCheckbox from '@/components/auth/AuthCheckbox.vue'
  import GoogleButton from '@/components/widgets/Buttons/GoogleButton.vue'
  import { useAuthStore } from '@/stores/authStore'
  import AuthPasswordField from '@/components/auth/AuthPasswordField.vue'
  import { useUserStore } from '@/stores/userStore'
  import { bootstrap } from '@/api/services/bootstrap'

  const router = useRouter()
  const authStore = useAuthStore()
  const userStore = useUserStore()

  const email = ref('')
  const password = ref('')
  const remember = ref(true)
  const errorMessage = ref('')
  const isSubmitting = ref(false)

  const canSubmit = computed(
    () => email.value.trim() !== '' && password.value.trim() !== '' && !isSubmitting.value,
  )

  async function handleSubmit() {
    if (!canSubmit.value) return

    errorMessage.value = ''
    isSubmitting.value = true
    try {
      await authStore.login({ login: email.value.trim(), password: password.value })
      if (!(await bootstrap())) return

      await router.push(userStore.user?.address ? { name: 'boardList' } : { name: 'address' })
    } catch (e) {
      errorMessage.value =
        isApiError(e) && e.status === 401 ? 'Invalid email or password.' : errorMessageFrom(e)
    } finally {
      isSubmitting.value = false
    }
  }
</script>
