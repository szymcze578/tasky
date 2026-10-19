<template>
  <div
    class="flex items-center gap-2 rounded-xl border px-4 py-3"
    :class="done ? 'border-line bg-cream-300' : 'border-line-warm bg-cream-50'"
  >
    <label class="relative flex h-6 w-6 flex-none items-center justify-center">
      <input
        type="checkbox"
        class="h-6 w-6 flex-none appearance-none justify-center rounded-lg cursor-pointer"
        :checked="done"
        :class="done ? 'bg-coral' : 'border-2 border-line-warm bg-cream-50'"
        @click="toggle"
      />
      <svg
        v-if="done"
        class="pointer-events-none absolute"
        width="14"
        height="14"
        viewBox="0 0 24 24"
        fill="none"
        stroke="#fff"
        stroke-width="3"
        stroke-linecap="round"
        stroke-linejoin="round"
      >
        <polyline points="20 6 9 17 4 12" />
      </svg>
    </label>
    <span class="flex text-[16px] font-semibold" :class="done ? 'text-ink-400' : 'text-ink'">{{
      task.name
    }}</span>
  </div>
</template>

<script setup lang="ts">
  import { TaskStatus, type Task } from '@/types/taskTypes'
  import { computed } from 'vue'

  const task = defineModel<Task>('task', { required: true })

  const done = computed(() => {
    return task.value.status == TaskStatus.Completed
  })

  function toggle() {
    task.value.status =
      task.value.status === TaskStatus.Completed ? TaskStatus.ToDo : TaskStatus.Completed
  }
</script>
