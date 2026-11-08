<template>
  <div
    class="relative isolate flex min-h-screen w-full flex-col items-center justify-center overflow-hidden bg-cream-200 p-6 font-sans"
  >
    <div class="absolute left-8 top-7 flex items-center gap-2.75">
      <TheLogo />
    </div>
    <div
      class="glow-coral pointer-events-none absolute -right-20 -top-30 h-90 w-90 rounded-full -z-10"
    />
    <div
      class="glow-amber pointer-events-none absolute -bottom-35 -left-25 h-100 w-100 rounded-full -z-10"
    />
    <router-view />
  </div>
</template>

<script setup lang="ts">
  import TheLogo from '@/components/icons/TheLogo.vue'
</script>

<style scoped>
  .glow-coral {
    background: radial-gradient(
      circle at 30% 30%,
      color-mix(in srgb, var(--color-coral) 16%, transparent),
      transparent 70%
    );
  }
  .glow-amber {
    background: radial-gradient(
      circle at 60% 40%,
      color-mix(in srgb, var(--color-accent-amber) 16%, transparent),
      transparent 70%
    );
  }
</style>
