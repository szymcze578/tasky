import { getBoardsForUser } from '@/api/board'
import type { Board } from '@/types/types'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useBoardStore = defineStore('board', () => {
  const boards = ref<Board[]>([])

  async function fetchBoards(userId: string) {
    try {
      boards.value = await getBoardsForUser(userId)
    } catch {
      console.error('Failed to fetch boards!')
    }
  }

  return { boards, fetchBoards }
})
