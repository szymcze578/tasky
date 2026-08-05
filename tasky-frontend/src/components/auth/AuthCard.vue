<template>
  <div
    class="relative flex flex-col min-h-screen w-full items-center justify-center overflow-hidden bg-cream-200 p-6 font-sans"
  >
    <div class="absolute left-8 top-7 flex items-center gap-2.75">
      <TheLogo />
    </div>
    <div class="glow-coral pointer-events-none absolute -right-20 -top-30 h-90 w-90 rounded-full" />
    <div
      class="glow-amber pointer-events-none absolute -bottom-35 -left-25 h-100 w-100 rounded-full"
    />
    <div class="flex w-full max-w-95 flex-col gap-4 rounded-xl border border-line bg-cream-50 p-8">
      <div class="flex flex-col w-full">
        <h1 class="font-display text-[27px] font-bold">{{ title }}</h1>
        <h2 class="font-sans text-[14px] text-ink-400 mt-2">
          <slot name="subtitle" />
        </h2>
      </div>
      <slot />
    </div>
    <p class="mt-5.5 text-center text-[14px] text-ink-500">
      <slot name="footer" />
    </p>
  </div>
</template>

<script setup lang="ts">
  import TheLogo from '@/components/icons/TheLogo.vue'

  defineProps<{ title: string }>()
</script>

<style scoped>
  .glow-coral {
    background: radial-gradient(
      circle at 30% 30%,
      rgba(247, 108, 75, 0.16),
      rgba(247, 108, 75, 0) 70%
    );
  }
  .glow-amber {
    background: radial-gradient(
      circle at 60% 40%,
      rgba(242, 166, 90, 0.16),
      rgba(242, 166, 90, 0) 70%
    );
  }
</style>
