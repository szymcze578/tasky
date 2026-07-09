<template>
  <aside class="flex h-screen w-66 flex-col border-r border-line bg-cream-50 px-4.5 py-6">
    <!-- Logo -->
    <TheLogo />
    <div class="flex flex-1 flex-col justify-between">
      <nav class="flex flex-col gap-0.75">
        <button
          v-for="item in nav"
          :key="item.id"
          type="button"
          class="flex font-display items-center gap-2.75 font-bold rounded-[11px] px-3 py-2.5 text-left text-[14.5px] transition-colors"
          :class="
            item.id === activeNav
              ? 'bg-coral-tint text-coral-600'
              : 'text-ink-600 hover:bg-cream-300'
          "
          @click="emit('select-nav', item.id)"
        >
          <span v-if="item.icon" class="flex h-5 w-5 items-center justify-center">
            <component :is="item.icon" />
          </span>
          <span class="flex-1">{{ item.label }}</span>
          <span
            v-if="item.count != null"
            class="rounded-pill px-2 py-0.5 text-[11.5px] font-display"
            :class="item.id === activeNav ? 'bg-coral text-white' : 'bg-line text-ink-400'"
            >{{ item.count }}</span
          >
        </button>
      </nav>
      <!-- Profile pill -->
      <button
        type="button"
        class="mt-auto flex items-center gap-2.75 rounded-2xl bg-cream-300 p-2.5 text-left"
        @click="emit('open-menu')"
      >
        <div
          class="flex h-9.5 w-9.5 flex-none items-center justify-center rounded-pill bg-coral-soft font-display text-[15px] font-bold text-coral"
        >
          {{ user.initials }}
        </div>
        <div class="min-w-0 flex-1">
          <div class="text-[13.5px] font-bold text-ink">{{ user.name }}</div>
          <div class="text-[11.5px] text-ink-400">{{ user.plan }}</div>
        </div>
        <svg
          width="18"
          height="18"
          viewBox="0 0 24 24"
          fill="none"
          stroke="#c9b6a9"
          stroke-width="2.2"
          stroke-linecap="round"
        >
          <circle cx="12" cy="5" r="1.4" />
          <circle cx="12" cy="12" r="1.4" />
          <circle cx="12" cy="19" r="1.4" />
        </svg>
      </button>
    </div>
  </aside>
</template>

<script setup lang="ts">
import type { NavItem, User } from '@/types/types'
import TheLogo from './icons/TheLogo.vue'

defineProps<{
  nav: NavItem[]
  activeNav: string
  user: User
}>()

const emit = defineEmits(['select-nav', 'open-menu'])
</script>
