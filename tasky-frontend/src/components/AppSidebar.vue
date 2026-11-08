<template>
  <aside class="flex h-screen w-66 flex-col border-r border-line bg-cream-50 px-4.5 py-6">
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
      <UserMenu />
    </div>
  </aside>
</template>

<script setup lang="ts">
  import type { NavItem } from '@/types/types'
  import TheLogo from './icons/TheLogo.vue'
  import type { UserDto } from '@/api/types.ts'
  import UserMenu from './layouts/app/UserMenu.vue'

  defineProps<{
    nav: NavItem[]
    activeNav: string
    user: UserDto
  }>()

  const emit = defineEmits(['select-nav', 'open-menu'])
</script>
