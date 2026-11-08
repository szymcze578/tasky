<template>
  <DropdownMenuRoot v-if="userStore.user">
    <DropdownMenuTrigger as-child>
      <button
        type="button"
        class="mt-auto flex w-full items-center gap-2.75 rounded-2xl bg-cream-300 p-2.5 text-left cursor-pointer"
      >
        <div
          class="flex h-9.5 w-9.5 flex-none items-center justify-center rounded-pill bg-coral-soft font-display text-[15px] font-bold text-coral"
        >
          {{ userStore.initials }}
        </div>
        <div class="min-w-0 flex-1">
          <div class="truncate text-[13.5px] font-bold text-ink">{{ userStore.user.userName }}</div>
          <div class="truncate text-[11.5px] text-ink-400">{{ userStore.user.email }}</div>
        </div>
        <EllipsisVertical :size="18" class="flex-none text-ink-300" aria-hidden="true" />
      </button>
    </DropdownMenuTrigger>

    <DropdownMenuPortal>
      <DropdownMenuContent
        side="top"
        align="start"
        :side-offset="8"
        class="z-50 w-56 rounded-2xl border-[1.5px] border-line-input bg-cream-50 p-1.5 shadow-panel"
      >
        <DropdownMenuItem as-child>
          <RouterLink to="/" :class="itemClass">
            <Settings :size="16" aria-hidden="true" /> Account settings
          </RouterLink>
        </DropdownMenuItem>

        <DropdownMenuSeparator class="my-1 h-px bg-line" />

        <DropdownMenuItem :class="[itemClass, 'text-danger']" @select="handleLogout">
          <LogOut :size="12" aria-hidden="true" /> Log out
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenuPortal>
  </DropdownMenuRoot>
</template>

<script setup lang="ts">
  import {
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuPortal,
    DropdownMenuRoot,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
  } from 'reka-ui'
  import { EllipsisVertical, LogOut, Settings } from '@lucide/vue'
  import { useAuthStore } from '@/stores/authStore'
  import { useBoardStore } from '@/stores/boardStore'
  import { useUserStore } from '@/stores/userStore'

  const authStore = useAuthStore()
  const boardStore = useBoardStore()
  const userStore = useUserStore()

  const itemClass =
    'flex cursor-pointer items-center gap-2.5 rounded-xl px-3 py-2.5 text-[14px] font-semibold text-ink outline-none data-[highlighted]:bg-cream-300'

  function handleLogout() {
    authStore.logout()
    boardStore.reset()
    userStore.reset()
  }
</script>
