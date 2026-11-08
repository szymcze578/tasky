<script setup lang="ts">
  import { isBootstrapping } from './api/services/bootstrap.ts'
  import TheLoader from './components/Loaders/TheLoader.vue'
</script>

<template>
  <RouterView />

  <Transition name="fade">
    <div v-if="isBootstrapping" class="fixed inset-0 z-50 grid place-items-center bg-cream-50">
      <TheLoader />
    </div>
  </Transition>
</template>

<style scoped>
  .fade-leave-active {
    transition: opacity 0.2s ease;
  }
  .fade-leave-to {
    opacity: 0;
  }
</style>
