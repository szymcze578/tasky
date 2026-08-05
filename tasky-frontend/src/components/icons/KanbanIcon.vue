<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2.2"
    stroke-linecap="round"
    stroke-linejoin="round"
  >
    <rect x="4" y="5" width="5" height="14" rx="1.5" />
    <rect x="10.5" y="5" width="5" height="9" rx="1.5" />
    <rect x="17" y="5" width="3.5" height="12" rx="1.5" />
  </svg>
</template>

<script setup lang="ts">
  withDefaults(
    defineProps<{
      size?: number
    }>(),
    {
      size: 20,
    },
  )
</script>
