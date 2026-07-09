<template>
  <div
    class="relative flex flex-col min-h-screen w-full items-center justify-center overflow-hidden bg-cream-200 p-6 font-sans"
  >
    <div class="absolute left-8 top-7 flex items-center gap-2.75">
      <TheLogo />
    </div>
    <div
      class="pointer-events-none absolute -right-20 -top-30 h-90 w-90 rounded-full"
      style="
        background: radial-gradient(
          circle at 30% 30%,
          rgba(247, 108, 75, 0.16),
          rgba(247, 108, 75, 0) 70%
        );
      "
    />
    <div
      class="pointer-events-none absolute -bottom-35 -left-25 h-100 w-100 rounded-full"
      style="
        background: radial-gradient(
          circle at 60% 40%,
          rgba(242, 166, 90, 0.16),
          rgba(242, 166, 90, 0) 70%
        );
      "
    />
    <form
      class="flex w-full max-w-95 flex-col gap-4 rounded-xl border border-line bg-cream-50 p-8"
      @submit.prevent="handleSubmit"
    >
      <div class="flex flex-col w-full">
        <h1 class="font-display text-[27px] font-bold">Welcome back</h1>
        <h2 class="font-sans text-[14px] text-ink-400 font-bold mt-2">
          Log in to pick up where you left off.
        </h2>
      </div>

      <label class="flex flex-col gap-1.5">
        <span class="text-[13px] text-ink-400">Email</span>
        <input
          v-model="email"
          type="email"
          autocomplete="email"
          class="rounded-xl border border-line-soft bg-cream-50 px-3.75 py-2.75 text-[14.5px] text-ink outline-none placeholder:text-ink-400"
        />
      </label>

      <label class="flex flex-col gap-1.5">
        <span class="text-[13px] text-ink-400">Password</span>
        <input
          v-model="password"
          type="password"
          autocomplete="current-password"
          class="rounded-xl border border-line-soft bg-cream-50 px-3.75 py-2.75 text-[14.5px] text-ink outline-none placeholder:text-ink-400"
        />
      </label>

      <button
        type="submit"
        :disabled="!isValid"
        class="mt-2 rounded-xl bg-ink px-3.75 py-2.75 text-[14.5px] font-medium text-cream-50 transition-colors disabled:cursor-not-allowed disabled:opacity-50"
      >
        Log in
      </button>
    </form>
    <p class="mt-5.5 text-center text-[14px] text-ink-500">
      New to Tasky?
      <a href="#" class="font-bold text-coral-600 no-underline">Create an account</a>
    </p>
  </div>
</template>

<script setup lang="ts">
import TheLogo from '@/components/icons/TheLogo.vue'
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const email = ref('')
const password = ref('')

const isValid = computed(() => email.value.trim() !== '' && password.value.trim() !== '')

function handleSubmit() {
  if (!isValid.value) return
  router.push('/')
}
</script>
