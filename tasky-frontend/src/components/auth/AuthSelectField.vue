<template>
  <div class="flex flex-col gap-1.5">
    <label :for="selectId" class="font-display text-[13px] font-bold text-ink-400">
      {{ label }}
    </label>

    <div class="relative">
      <span
        v-if="icon"
        class="pointer-events-none absolute left-3.75 top-1/2 -translate-y-1/2 text-ink-300"
        aria-hidden="true"
      >
        <component :is="icon" :size="18" />
      </span>

      <select
        :id="selectId"
        v-model="modelValue"
        v-bind="$attrs"
        :aria-invalid="!!error"
        :aria-describedby="error ? errorId : undefined"
        class="w-full appearance-none rounded-2xl border-[1.5px] bg-cream-100 py-3.25 pr-11 text-base outline-none transition-all focus:bg-cream-50 focus:ring-4 tablet:text-[15px]"
        :class="[
          icon ? 'pl-11' : 'pl-3.75',
          modelValue ? 'text-ink' : 'text-ink-400',
          stateClasses,
        ]"
      >
        <option value="" disabled>{{ placeholder }}</option>
        <option v-for="opt in options" :key="opt.value" :value="opt.value" class="text-ink">
          {{ opt.label }}
        </option>
      </select>

      <ChevronDown
        class="pointer-events-none absolute right-3.75 top-1/2 -translate-y-1/2 text-ink-300"
        :size="18"
        aria-hidden="true"
      />
    </div>

    <p v-if="error" :id="errorId" role="alert" class="text-[12.5px] text-danger">{{ error }}</p>
  </div>
</template>

<script setup lang="ts">
  import { computed, useId, type Component } from 'vue'
  import { ChevronDown } from '@lucide/vue'

  defineOptions({ inheritAttrs: false })

  const props = defineProps<{
    label: string
    options: { value: string; label: string }[]
    placeholder?: string
    icon?: Component
    error?: string
  }>()

  const modelValue = defineModel<string>({ required: true })

  const selectId = useId()
  const errorId = useId()

  const stateClasses = computed(() =>
    props.error
      ? 'border-danger focus:ring-danger/10'
      : 'border-line-input focus:border-coral-focus focus:ring-coral/10',
  )
</script>

<style scoped>
  @media (hover: hover) and (pointer: fine) {
    @supports (appearance: base-select) {
      select,
      select::picker(select) {
        appearance: base-select;
      }

      select::picker(select) {
        margin-top: 6px;
        padding: 6px;
        border: 1.5px solid var(--color-line-input);
        border-radius: var(--radius-2xl);
        background: var(--color-cream-50);
        box-shadow: var(--shadow-panel);
      }

      select option {
        padding: 10px 12px;
        border-radius: var(--radius-xl);
        color: var(--color-ink);
      }

      select option:hover {
        background: var(--color-cream-300);
      }

      select option:checked {
        background: var(--color-coral-tint);
        font-weight: 700;
      }

      /* rysujemy własną strzałkę (ChevronDown), więc ukrywamy natywną */
      select::picker-icon {
        display: none;
      }
    }
  }
</style>
