<template>
  <AuthCard title="Create your account">
    <template #subtitle>Start organizing your day in minutes.</template>
    <form class="flex flex-col gap-4" @submit.prevent="handleRegistration">
      <AuthTextField v-model="name" label="Full name" autocomplete="name" :icon="User" />
      <AuthTextField v-model="email" label="Email" type="email" autocomplete="email" :icon="Mail" />
      <AuthTextField
        v-model="password"
        label="Password"
        type="password"
        autocomplete="new-password"
        :icon="Lock"
      />
      <AuthCheckbox v-model="terms" />
      <button
        type="submit"
        :disabled="!isValid"
        class="mt-2 rounded-xl bg-ink px-3.75 py-2.75 text-[14.5px] font-medium text-cream-50 transition-colors disabled:cursor-not-allowed disabled:opacity-50"
      >
        Create account
      </button>
      <div class="flex items-center gap-3.5">
        <div class="h-px flex-1 bg-line" />
        <span class="text-[12px] font-semibold text-ink-300">or</span>
        <div class="h-px flex-1 bg-line" />
      </div>

      <button
        type="button"
        class="flex w-full items-center justify-center gap-2.75 rounded-xl border border-line-soft bg-white p-3.5 text-[14.5px] font-bold text-ink-500 transition-colors hover:bg-cream-300"
        @click="handleGoogle"
      >
        <svg width="19" height="19" viewBox="0 0 24 24">
          <path
            fill="#EA4335"
            d="M12 10.2v3.9h5.5c-.24 1.4-1.7 4.1-5.5 4.1-3.3 0-6-2.7-6-6.1S8.7 5.9 12 5.9c1.9 0 3.1.8 3.8 1.5l2.6-2.5C16.8 3.3 14.6 2.3 12 2.3 6.9 2.3 2.8 6.4 2.8 11.5S6.9 20.7 12 20.7c5.3 0 8.8-3.7 8.8-9 0-.6-.06-1-.15-1.5H12z"
          />
        </svg>
        <span>Continue with Google</span>
      </button>
    </form>

    <template #footer>
      Already have an account?
      <a href="/login" class="font-bold text-coral-600 no-underline">Log in</a>
    </template>
  </AuthCard>
</template>

<script setup lang="ts">
  import AuthCard from '@/components/auth/AuthCard.vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'
  import AuthCheckbox from '@/components/auth/AuthCheckbox.vue'
  import { computed, ref } from 'vue'
  import { registerUser } from '@/api/auth'
  import { useRouter } from 'vue-router'
  import { Lock, Mail, User } from '@lucide/vue'

  const router = useRouter()

  const name = ref('')
  const email = ref('')
  const password = ref('')
  const terms = ref(false)

  const isValid = computed(
    () =>
      name.value.trim() !== '' &&
      email.value.trim() !== '' &&
      password.value.trim() !== '' &&
      terms.value,
  )

  async function handleRegistration() {
    if (!isValid.value) {
      return
    }
    try {
      await registerUser({ userName: name.value, email: email.value, password: password.value })
      router.push('/login')
    } catch {
      console.error('Failed to register user!')
    }
  }

  function handleGoogle() {
    console.log('Registering by google!')
  }
</script>
