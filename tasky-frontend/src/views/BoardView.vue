<template>
  <div class="flex h-screen w-full overflow-hidden bg-cream-200 font-sans">
    <AppSidebar
      v-if="userStore.user"
      class="hidden tablet:flex"
      :nav="navItems"
      active-nav="all"
      :user="userStore.user"
    />
    <main class="flex min-w-0 flex-1 flex-col">
      <AppTopbar v-model:search="search" class="hidden tablet:flex" />

      <div class="scroll-warm flex-1 overflow-y-auto px-5 py-6 tablet:px-10 tablet:py-9.5">
        <router-view />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import AppSidebar from '@/components/AppSidebar.vue'
import { computed, ref } from 'vue'
import { useUserStore } from '@/stores/userStore'
import { LayoutGrid } from '@lucide/vue'
import AppTopbar from '@/components/AppTopbar.vue'
import { useBoardStore } from '@/stores/boardStore'

const userStore = useUserStore()
const boardStore = useBoardStore()

const search = ref('')

const boardsCount = computed(() => boardStore.boards.length)

const navItems = computed(() => [
  {
    id: 'all',
    label: 'All boards',
    icon: LayoutGrid,
    count: boardsCount.value,
  },
])
</script>
