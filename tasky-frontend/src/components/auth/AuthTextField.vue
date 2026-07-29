<template>
  <label class="flex flex-col gap-1.5">
    <span class="text-[13px] text-ink-400 font-bold font-display">{{ label }}</span>
    <div
      class="flex items-center gap-2 rounded-xl border border-line-soft bg-cream-200 px-3.75 py-2.75"
    >
      <span v-if="icon" class="flex h-4.5 w-4.5 items-center justify-center text-ink-400">
        <component :is="icon" :size="18" />
      </span>
      <input
        v-model="modelValue"
        :type="type"
        :placeholder="placeholder"
        :autocomplete="autocomplete"
        class="flex-1 border-none bg-transparent text-[14.5px] text-ink outline-none placeholder:text-ink-400"
      />
      <slot name="trailing" />
    </div>
  </label>
</template>

<script setup lang="ts">
import type { Component } from 'vue'

defineProps<{
  label: string
  type?: string
  autocomplete?: string
  placeholder?: string
  icon?: Component
}>()

const modelValue = defineModel()
</script>
