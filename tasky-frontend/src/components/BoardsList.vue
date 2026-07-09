<template>
  <div>
    <div class="mb-3.5 ml-0.5 text-[11.5px] fnont-bold tracking-[0.08em] text-ink-300">
      YOUR BOARDS
    </div>

    <div class="scroll-warm flex max-h-140 flex-col gap-2.75 py-2">
      <BoardCard
        v-for="board in boards"
        :key="board.id"
        :board="board"
        @click="$emit('open', board)"
      />

      <div
        v-if="boards.length === 0"
        class="rounded-3xl border-[1.5px] border-dashed border-line-input bg-cream-50 px-5 py-10 text-center text-[14px] text-ink-400"
      >
        No boards yet — create your first one above.
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Board } from '@/types/types'
import BoardCard from './BoardCard.vue'

defineProps<{
  boards: Board[]
}>()

defineEmits(['open'])
</script>
