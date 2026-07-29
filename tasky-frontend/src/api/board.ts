import { apiClient } from './client'
import type { components } from './schema'
import { BoardType, type Board } from '@/types/types'

export type CreateBoardViewModel = components['schemas']['CreateBoardViewModel']
export type BoardViewModel = components['schemas']['BoardViewModel']

function mapBoard(response: BoardViewModel): Board {
  return {
    id: response.id ?? '',
    name: response.name ?? '',
    type: response.type ?? 0,
    editedAt: response.updateDate ?? '',
  }
}

export async function getBoardsForUser(UserId: string): Promise<Board[]> {
  const { data } = await apiClient.get<BoardViewModel[]>('/api/board', { params: { UserId } })
  return data.map(mapBoard)
}

export async function createBoard(payload: CreateBoardViewModel): Promise<Board> {
  const { data } = await apiClient.post<BoardViewModel>('/api/board', payload)
  return mapBoard(data)
}
