<template>
  <div class="mx-auto w-full max-w-180">
    <BoardListHeader :count="boardStore.boards.length" @create="showCreate = true" />
    <CreateBoardCard v-if="showCreate" @create="handleCreate" @cancel="showCreate = false" />
    <BoardsList :boards="boardStore.boards" @open="openBoard()" />
  </div>
</template>

<script setup lang="ts">
  import type { BoardType } from '@/types/types'
  import BoardListHeader from '@/components/BoardListHeader.vue'
  import { ref } from 'vue'
  import CreateBoardCard from '@/components/CreateBoardCard.vue'
  import BoardsList from '@/components/BoardsList.vue'
  import { createBoard, type CreateBoardViewModel } from '@/api/board'
  import { useBoardStore } from '@/stores/boardStore'
  import { useUserStore } from '@/stores/userStore'
  import router from '@/router'

  const boardStore = useBoardStore()
  const userStore = useUserStore()

  //const emit = defineEmits(['open-board'])

  const showCreate = ref(false)

  async function handleCreate(payload: string, boardType: BoardType) {
    const boardToCreate: CreateBoardViewModel = {
      name: payload,
      description: '',
      type: boardType,
      ownerId: userStore.user?.userId,
    }

    await createBoard(boardToCreate)
      .then(() => {
        showCreate.value = false
        if (userStore.user?.userId) boardStore.fetchBoards(userStore.user?.userId)
      })
      .catch(() => console.warn('Failed to create new board!'))
  }

  function openBoard() {
    router.push('/boardId')
  }
</script>
