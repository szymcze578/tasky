<template>
  <div class="flex flex-col gap-1.5">
    <label class="flex flex-col gap-1.5">
      <span class="font-display text-[13px] font-bold text-ink-400">{{ label }}</span>

      <div
        class="flex items-center gap-2.75 rounded-2xl border-[1.5px] bg-cream-100 px-3.75 py-3.25 transition-all focus-within:bg-cream-50"
        :class="stateClasses"
      >
        <span
          v-if="icon"
          class="flex h-4.5 w-4.5 items-center justify-center text-ink-300"
          aria-hidden="true"
        >
          <component :is="icon" :size="18" />
        </span>

        <input
          v-model="modelValue"
          v-bind="$attrs"
          :aria-invalid="!!error"
          :aria-describedby="error ? errorId : undefined"
          class="flex-1 border-none bg-transparent text-base text-ink outline-none placeholder:text-ink-400 tablet:text-[15px]"
        />

        <slot name="trailing" />
      </div>
    </label>

    <p v-if="error" :id="errorId" role="alert" class="text-[12.5px] text-red-600">{{ error }}</p>
  </div>
</template>

<script setup lang="ts">
  import { computed, useId, type Component } from 'vue'

  defineOptions({ inheritAttrs: false })

  const props = defineProps<{
    label: string
    icon?: Component
    error?: string
  }>()

  const modelValue = defineModel<string>({ required: true })

  const errorId = useId()

  const stateClasses = computed(() =>
    props.error
      ? 'border-danger focus-within:ring-4 focus-within:ring-danger/10'
      : 'border-line-input focus-within:border-coral-focus focus-within:ring-4 focus-within:ring-coral/10',
  )
</script>
