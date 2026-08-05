<template>
  <label class="flex flex-col gap-1.5">
    <span class="text-[13px] text-ink-400 font-bold font-display">{{ label }}</span>
    <div
      class="flex items-center gap-2.75 rounded-[15px] border-[1.5px] px-3.75 py-3.25 transition-all"
      :class="
        focused
          ? 'border-[#F0A88E] bg-cream-50 shadow-[0_0_0_4px_rgba(247,108,75,0.1)]'
          : 'border-line-input bg-[#FCF7F3]'
      "
    >
      <span v-if="icon" class="flex h-4.5 w-4.5 items-center justify-center text-ink-300">
        <component :is="icon" :size="18" />
      </span>
      <input
        v-model="modelValue"
        :type="type"
        :placeholder="placeholder"
        :autocomplete="autocomplete"
        class="flex-1 border-none bg-transparent text-[15px] text-ink outline-none placeholder:text-ink-400"
        @focus="focused = true"
        @blur="focused = false"
      />
      <slot name="trailing" />
    </div>
  </label>
</template>

<script setup lang="ts">
  import { ref, type Component } from 'vue'

  defineProps<{
    label: string
    type?: string
    autocomplete?: string
    placeholder?: string
    icon?: Component
  }>()

  const modelValue = defineModel()

  const focused = ref(false)
</script>
