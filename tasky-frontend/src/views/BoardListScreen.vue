<template>
  <div class="mx-auto w-full max-w-180">
    <BoardListHeader :count="boardStore.boards.length" @create="showCreate = true" />
    <CreateBoardCard v-if="showCreate" @create="handleCreate" @cancel="showCreate = false" />
    <BoardsList :boards="boardStore.boards" @open="(boardId) => openBoard(boardId)" />
  </div>
</template>

<script setup lang="ts">
  import BoardListHeader from '@/components/BoardListHeader.vue'
  import { ref } from 'vue'
  import CreateBoardCard from '@/components/CreateBoardCard.vue'
  import BoardsList from '@/components/BoardsList.vue'
  import { useBoardStore } from '@/stores/boardStore'
  import router from '@/router'
  import { isApiError } from '@/api/errors'
  import type { BoardType } from '@/api/types'

  const boardStore = useBoardStore()

  const showCreate = ref(false)
  const createError = ref<string | null>(null)

  async function handleCreate(name: string, boardType: BoardType) {
    createError.value = null
    try {
      await boardStore.createBoard({ name, type: boardType })
      showCreate.value = false
    } catch (e) {
      createError.value = isApiError(e) ? e.message : 'Failed to create board'
    }
  }

  function openBoard(boardId: string) {
    router.push({ name: 'board', params: { boardId } })
  }
</script>
