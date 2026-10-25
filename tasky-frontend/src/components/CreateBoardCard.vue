<template>
  <div
    class="mb-5.5 animate-slide-down rounded-4xl border border-line-input bg-cream-50 p-5.5 shadow-panel"
  >
    <h2 class="mb-4 font-display text-[18px] font-bold text-ink">Create a new board</h2>

    <div class="flex flex-col items-start gap-4 sm:flex-row">
      <div class="w-full min-w-0 flex-1">
        <label class="mb-2 block text-[12.5px] font-bold text-ink-400">Board name</label>
        <input
          v-model="name"
          type="text"
          autofocus
          placeholder="e.g. Marketing Launch…"
          class="w-full rounded-[13px] border-[1.5px] border-line-input bg-white px-3.75 py-3.25 text-[15px] font-medium text-ink outline-none"
          @keydown.enter="submit"
        />
      </div>

      <div class="w-full flex-none sm:w-auto">
        <label class="mb-2 block text-[12.5px] font-bold text-ink-400">Board type</label>
        <div class="flex w-full gap-2 sm:w-auto">
          <button
            type="button"
            :class="segClass(type === BoardTypes.List)"
            @click="type = BoardTypes.List"
          >
            <ListIcon :size="16" />
            <span>List</span>
          </button>
          <button
            type="button"
            :class="segClass(type === BoardTypes.Kanban)"
            @click="type = BoardTypes.Kanban"
          >
            <KanbanIcon :size="16" />
            <span>Kanban</span>
          </button>
        </div>
      </div>
    </div>

    <div class="mt-5 flex justify-end gap-2.5">
      <button
        type="button"
        class="flex-none rounded-xl border-none bg-line-warm px-5 py-3 text-[14px] font-bold text-ink-500 hover:brightness-95"
        @click="cancel"
      >
        Cancel
      </button>
      <button
        type="button"
        class="rounded-xl border-none bg-coral px-6 py-3 text-[14px] font-bold text-white shadow-coral-sm transition-[filter] hover:brightness-105"
        @click="submit"
      >
        Create board
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import KanbanIcon from './icons/KanbanIcon.vue'
  import ListIcon from './icons/ListIcon.vue'
  import type { BoardType } from '@/api/types.ts'
  import { BoardTypes } from '@/types/types.ts'

  const emit = defineEmits<{
    (e: 'create', name: string, type: BoardType): void
    (e: 'cancel'): void
  }>()

  const name = ref('')
  const type = ref<BoardType>(BoardTypes.List)

  function submit() {
    const trimmed = name.value.trim()
    if (!trimmed) return
    emit('create', trimmed, type.value)
    name.value = ''
    type.value = BoardTypes.List
  }

  function cancel() {
    name.value = ''
    type.value = BoardTypes.List
    emit('cancel')
  }

  function segClass(active: boolean) {
    return [
      'flex flex-1 items-center justify-center gap-2 rounded-xl border-none py-[11px] text-[14px] font-bold transition-all sm:flex-none sm:px-4 hover:brightness-95',
      active ? 'bg-coral text-white' : 'bg-blush text-ink-tag',
    ]
  }
</script>
