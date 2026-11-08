<template>
  <AuthCard class="min-w-105">
    <template #title>Create your account</template>
    <template #subtitle>Start organizing your day in minutes.</template>
    <template #content>
      <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
        <AuthTextField
          v-model="name"
          label="Full name"
          placeholder="Enter your full name"
          autocomplete="name"
          :icon="User"
        />
        <AuthTextField
          v-model="email"
          label="Email"
          placeholder="Enter email"
          type="email"
          autocomplete="email"
          :icon="Mail"
        />
        <AuthPasswordField
          v-model="password"
          label="Password"
          placeholder="••••••••"
          autocomplete="new-password"
        />
        <AuthCheckbox v-model="terms">
          <span class="text-[13.5px] font-semibold leading-[1.4] text-ink-600">
            I agree to the
            <a href="#" class="font-bold text-coral-600 no-underline">Terms</a>
            and
            <a href="#" class="font-bold text-coral-600 no-underline">Privacy Policy</a>
          </span>
        </AuthCheckbox>
        <p v-if="errorMessage" role="alert" class="text-[13px] text-red-600">{{ errorMessage }}</p>
        <AuthSubmitButton :disabled="!canSubmit">Create account</AuthSubmitButton>
        <AuthDivider />
        <GoogleButton />
      </form>
    </template>
    <template #footer>
      Already have an account?
      <RouterLink to="/login" class="font-bold text-coral-600 no-underline">Log in</RouterLink>
    </template>
  </AuthCard>
</template>

<script setup lang="ts">
  import { computed, ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { Mail, User } from '@lucide/vue'
  import { authApi } from '@/api/auth'
  import { errorMessageFrom } from '@/api/errors'
  import AuthCard from '@/components/auth/AuthCard.vue'
  import AuthCheckbox from '@/components/auth/AuthCheckbox.vue'
  import AuthDivider from '@/components/auth/AuthDivider.vue'
  import AuthSubmitButton from '@/components/auth/AuthSubmitButton.vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'
  import GoogleButton from '@/components/widgets/Buttons/GoogleButton.vue'
  import AuthPasswordField from '@/components/auth/AuthPasswordField.vue'

  const router = useRouter()

  const name = ref('')
  const email = ref('')
  const password = ref('')
  const terms = ref(false)
  const errorMessage = ref('')
  const isSubmitting = ref(false)

  const canSubmit = computed(
    () =>
      name.value.trim() !== '' &&
      email.value.trim() !== '' &&
      password.value.trim() !== '' &&
      terms.value &&
      !isSubmitting.value,
  )

  async function handleSubmit() {
    if (!canSubmit.value) return

    errorMessage.value = ''
    isSubmitting.value = true
    try {
      await authApi.register({
        userName: name.value.trim(),
        email: email.value.trim(),
        password: password.value,
      })
      await router.push({ name: 'login' })
    } catch (e) {
      errorMessage.value = errorMessageFrom(e)
    } finally {
      isSubmitting.value = false
    }
  }
</script>
