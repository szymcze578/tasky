<template>
  <div class="flex min-w-95 flex-col items-center">
    <div
      class="flex w-full flex-col gap-4 rounded-xl border border-line bg-cream-50 p-8 shadow-screen"
    >
      <header>
        <h1 class="font-display text-[27px] leading-none font-bold">
          <slot name="title" />
        </h1>
        <p v-if="$slots.subtitle" class="mt-2 text-[14px] text-ink-400">
          <slot name="subtitle" />
        </p>
      </header>
      <slot name="content" />
    </div>
    <p v-if="$slots.footer" class="mt-5.5 text-center text-[14px] text-ink-500">
      <slot name="footer" />
    </p>
  </div>
</template>

<script setup lang="ts">
  defineProps<{ title?: string }>()
</script>
