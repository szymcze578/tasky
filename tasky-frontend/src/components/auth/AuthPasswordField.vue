<template>
  <AuthTextField
    v-model="modelValue"
    :label="label"
    :error="error"
    :icon="Lock"
    :type="revealed ? 'text' : 'password'"
  >
    <template #trailing>
      <button
        type="button"
        class="text-ink-300 transition-colors hover:text-ink-500"
        :aria-label="revealed ? 'Hide password' : 'Show password'"
        :aria-pressed="revealed"
        @click="revealed = !revealed"
      >
        <component :is="revealed ? EyeOff : Eye" :size="18" />
      </button>
    </template>
  </AuthTextField>
</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import { Eye, EyeOff, Lock } from '@lucide/vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'

  defineProps<{
    label: string
    error?: string
  }>()

  const modelValue = defineModel<string>({ required: true })

  const revealed = ref(false)
</script>
