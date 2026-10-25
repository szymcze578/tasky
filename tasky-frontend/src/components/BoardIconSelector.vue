<template>
  <div v-if="type === BoardTypes.Kanban">
    <KanbanIcon :size="size" />
  </div>
  <div v-else-if="type === BoardTypes.List">
    <ListIcon :size="size" />
  </div>
</template>

<script setup lang="ts">
  import { BoardTypes } from '@/types/types'
  import KanbanIcon from './icons/KanbanIcon.vue'
  import ListIcon from './icons/ListIcon.vue'

  defineProps<{ type?: string; size?: number }>()
</script>
