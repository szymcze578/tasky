<template>
  <div class="mb-6 flex items-end justify-between gap-4">
    <div>
      <h1 class="font-display text-[34px] font-bold leading-none text-ink">Boards</h1>
      <p class="mt-2 text-[14px] text-ink-400">
        <span>{{ count }}</span>
        <span>{{ compact ? ' active boards' : ' active boards in your workspace' }}</span>
      </p>
    </div>

    <button
      type="button"
      class="flex flex-none items-center gap-2.25 rounded-2xl bg-coral px-5 py-3.25 text-[15px] font-bold text-white shadow-coral transition-[filter] hover:brightness-105"
      @click="emit('create')"
    >
      <svg
        width="18"
        height="18"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="2.6"
        stroke-linecap="round"
      >
        <line x1="12" y1="6" x2="12" y2="18" />
        <line x1="6" y1="12" x2="18" y2="12" />
      </svg>
      <span>New board</span>
    </button>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  count: number
  compact?: boolean
}>()

const emit = defineEmits(['create'])
</script>
