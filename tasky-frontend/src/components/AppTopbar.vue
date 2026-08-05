<template>
  <header class="flex flex-none items-center gap-4 border-b border-line bg-cream-200 px-10 py-5">
    <!-- Search -->
    <label
      class="flex max-w-105 flex-1 items-center gap-2.5 rounded-xl border border-line-soft bg-cream-50 px-3.75 py-2.75"
    >
      <svg
        width="18"
        height="18"
        viewBox="0 0 24 24"
        fill="none"
        stroke="#c9b6a9"
        stroke-width="2.4"
        stroke-linecap="round"
      >
        <circle cx="11" cy="11" r="7" />
        <line x1="21" y1="21" x2="16.65" y2="16.65" />
      </svg>
      <input
        v-model="search"
        type="text"
        placeholder="Search boards…"
        class="flex-1 border-none bg-transparent text-[14.5px] text-ink outline-none placeholder:text-ink-400"
      />
    </label>

    <div class="flex-1" />

    <!-- Notifications -->
    <button
      type="button"
      class="relative flex h-10.5 w-10.5 items-center justify-center rounded-xl border border-line-soft bg-cream-50 transition-colors hover:bg-cream-300"
      @click="emit('notifications')"
    >
      <svg
        width="19"
        height="19"
        viewBox="0 0 24 24"
        fill="none"
        stroke="#8c7a6e"
        stroke-width="2.2"
        stroke-linecap="round"
        stroke-linejoin="round"
      >
        <path d="M18 8A6 6 0 0 0 6 8c0 7-3 9-3 9h18s-3-2-3-9" />
        <path d="M13.7 21a2 2 0 0 1-3.4 0" />
      </svg>
      <span
        v-if="hasNotifications"
        class="absolute right-2.25 top-2.25 h-2 w-2 rounded-pill border-2 border-cream-50 bg-coral"
      />
    </button>
  </header>
</template>

<script setup lang="ts">
  defineProps<{
    hasNotifications?: boolean
  }>()

  const search = defineModel('search')

  const emit = defineEmits(['update:search', 'notifications'])
</script>
