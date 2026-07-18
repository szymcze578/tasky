<template>
  <label class="flex flex-col gap-1.5">
    <span class="text-[13px] text-ink-400 font-bold font-display">{{ label }}</span>
    <div
      class="flex items-center gap-2 rounded-xl border border-line-soft bg-cream-200 px-3.75 py-2.75"
    >
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
defineProps<{
  label: string
  type?: string
  autocomplete?: string
  placeholder?: string
}>()

const modelValue = defineModel()
</script>
