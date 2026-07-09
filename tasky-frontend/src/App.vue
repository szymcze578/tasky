<template>
  <router-view />
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useBoardStore } from './stores/boardStore'
import { useUserStore } from './stores/userStore'

const userStore = useUserStore()
const boardStore = useBoardStore()
const isLoading = ref(true)

onMounted(async () => {
  try {
    await userStore.fetchUser()
    if (userStore.user) {
      await boardStore.fetchBoards(userStore.user.userId)
    }
  } catch {
    console.warn('Failed to initialize app data! Login needed...')
  } finally {
    isLoading.value = false
  }
})
</script>
