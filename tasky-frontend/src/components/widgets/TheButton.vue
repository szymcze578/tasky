<template>
  <div class="flex flex-col gap-2 cursor-pointer">
    <span v-if="icon" class="flex h-4.5 w-4.5 items-center justify-center text-ink-400">
      <component :is="icon" :size="18" />
    </span>
    <button
      type="button"
      :class="buttonStyle"
      class="flex-none rounded-xl border-none px-5.5 py-3 text-[14px] font-bold"
      @click="$emit('onClick')"
    >
      {{ text }}
    </button>
  </div>
</template>

<script setup lang="ts">
  import { ButtonType } from '@/types/types'
  import { computed, type Component } from 'vue'

  const props = defineProps<{
    text: string
    icon?: Component
    type: ButtonType
  }>()

  defineEmits<{
    (e: 'onClick'): void
  }>()

  const buttonStyle = computed(() => {
    if (props.type == ButtonType.White) {
      return 'bg-blush text-ink-tag transition-[filter] hover:brightness-95'
    } else if (props.type == ButtonType.Orange) {
      return 'bg-coral text-white shadow-coral-sm transition-[filter] hover:brightness-105'
    } else {
      return 'bg-blush text-ink-tag transition-[filter] hover:brightness-95'
    }
  })
</script>
