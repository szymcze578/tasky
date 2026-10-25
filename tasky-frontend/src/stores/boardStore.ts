import { boardsApi } from '@/api/board'
import { isApiError } from '@/api/errors'
import type { Board, CreateBoardRequest } from '@/api/types'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useBoardStore = defineStore('board', () => {
  const boards = ref<Board[]>([])
  const isLoading = ref(false)
  const error = ref<string | null>(null)

  async function fetchBoards() {
    isLoading.value = true
    error.value = null
    try {
      boards.value = await boardsApi.getAll()
    } catch (e) {
      error.value = isApiError(e) ? e.message : 'Failed to fetch boards'
      console.error('Failed to fetch boards', e)
    } finally {
      isLoading.value = false
    }
  }

  async function createBoard(request: CreateBoardRequest) {
    const board = await boardsApi.create(request)
    boards.value.push(board) // odpowiedź to pełny BoardDto, więc nie trzeba pobierać listy od nowa
    return board
  }

  function getBoardById(boardId: string): Board | undefined {
    return boards.value.find((b) => b.id === boardId)
  }

  async function ensureBoard(boardId: string): Promise<Board> {
    const cached = getBoardById(boardId)
    if (cached) return cached

    const board = await boardsApi.get(boardId)
    boards.value.push(board)
    return board
  }

  function reset() {
    boards.value = []
    error.value = null
  }

  return { boards, isLoading, error, fetchBoards, createBoard, getBoardById, ensureBoard, reset }
})
