<template>
  <div
    class="flex cursor-pointer items-center gap-4 rounded-3xl border border-line-warm bg-cream-50 p-4 transition-all hover:-translate-y-px hover:border-[#F0C9B8] hover:shadow-soft"
    @click="$emit('select-board', board)"
  >
    <div
      class="flex h-11.5 w-11.5 flex-none items-center justify-center rounded-2xl bg-coral/10 text-coral"
    >
      <BoardIconSelector :type="board.type" />
    </div>
    <div class="min-w-0 flex-1">
      <div class="truncate text-[16.5px] font-bold text-ink">{{ board.name }}</div>
      <div class="mt-1.25 flex items-center gap-2.5">
        <span
          class="inline-flex items-center rounded-pill bg-blush px-2.5 py-0.75 text-[11.5px] font-bold text-ink-tag"
        >
          {{ board.type === BoardType.Kanban ? 'Kanban' : 'List' }}
        </span>
        <span class="text-[13px] text-ink-400">Edited {{ dayjs(board.editedAt).fromNow() }}</span>
      </div>
    </div>

    <ArrowIcon />
  </div>
</template>

<script setup lang="ts">
import { BoardType, type Board } from '@/types/types'
import dayjs from 'dayjs'
import ArrowIcon from './icons/ArrowIcon.vue'
import BoardIconSelector from './BoardIconSelector.vue'

defineProps<{
  board: Board
}>()

defineEmits(['select-board'])
</script>
