<template>
  <button
    type="submit"
    :disabled="disabled"
    class="mt-2 rounded-xl bg-ink px-3 py-2.5 text-[14.5px] font-medium text-cream-50 transition-colors disabled:cursor-not-allowed disabled:opacity-50 cursor-pointer"
  >
    <slot />
  </button>
</template>

<script setup lang="ts">
  defineProps<{ disabled?: boolean }>()
</script>
