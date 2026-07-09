<template>
  <div v-if="type === BoardType.Kanban">
    <KanbanIcon :size="size" />
  </div>
  <div v-else-if="type === BoardType.List">
    <ListIcon :size="size" />
  </div>
</template>

<script setup lang="ts">
import { BoardType } from '@/types/types'
import KanbanIcon from './icons/KanbanIcon.vue'
import ListIcon from './icons/ListIcon.vue'

defineProps<{ type: BoardType; size?: number }>()
</script>
