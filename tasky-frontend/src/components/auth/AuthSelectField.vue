<template>
  <label class="flex flex-col gap-1.5">
    <span class="text-[13px] text-ink-400">{{ label }}</span>
    <div
      class="flex items-center gap-2 rounded-xl border border-line-soft bg-cream-200 px-3.75 py-2.75"
    >
      <span v-if="icon" class="flex flex-none text-ink-300">
        <component :is="icon" :size="18" />
      </span>
      <select
        :value="modelValue"
        class="flex-1 appearance-none border-none bg-transparent text-[14.5px] text-ink outline-none"
        @change="$emit('update:modelValue', ($event.target as HTMLSelectElement).value)"
      >
        <option value="">{{ placeholder }}</option>
        <option v-for="opt in options" :key="opt.value" :value="opt.value">{{ opt.label }}</option>
      </select>
      <svg
        width="16"
        height="16"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="2.4"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="flex-none text-ink-300"
        style="pointer-events: none"
      >
        <polyline points="6 9 12 15 18 9" />
      </svg>
    </div>
  </label>
</template>

<script setup lang="ts">
  import type { Component } from 'vue'

  defineProps<{
    modelValue: string
    label: string
    placeholder?: string
    options: { value: string; label: string }[]
    icon?: Component
  }>()
  defineEmits<{ 'update:modelValue': [value: string] }>()
</script>
