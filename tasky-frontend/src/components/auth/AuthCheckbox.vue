<template>
  <label class="flex cursor-pointer select-none items-start gap-2.5">
    <span
      class="mt-px flex h-5 w-5 flex-none items-center justify-center rounded-[7px] border-[1.5px] transition-all"
      :class="modelValue ? 'border-coral bg-coral' : 'border-[#E3D2C6] bg-white'"
      @click="() => (modelValue = !modelValue)"
    >
      <svg
        v-if="modelValue"
        width="13"
        height="13"
        viewBox="0 0 24 24"
        fill="none"
        stroke="#fff"
        stroke-width="3.2"
        stroke-linecap="round"
        stroke-linejoin="round"
      >
        <polyline points="20 6 9 17 4 12" />
      </svg>
    </span>
    <slot />
  </label>
</template>

<script setup lang="ts">
  const modelValue = defineModel({ default: false })
</script>
