import { apiClient } from './client'
import type { components } from './schema'
import { BoardType, type Board } from '@/types/types'

export type CreateBoardViewModel = components['schemas']['CreateBoardViewModel']

// The backend doesn't declare response types (no [ProducesResponseType]), so
// Swagger/schema.d.ts has no schema for these responses. BoardResponse below
// reflects Tasky.API.ViewModels.BoardViewModel by hand until that's fixed.
interface BoardResponse {
  id: string
  name: string
  type: BoardType
  updateDate: string
}

function mapBoard(response: BoardResponse): Board {
  return {
    id: response.id,
    name: response.name,
    type: response.type,
    editedAt: response.updateDate,
  }
}

export async function getBoardsForUser(UserId: string): Promise<Board[]> {
  const { data } = await apiClient.get<BoardResponse[]>('/api/board', { params: { UserId } })
  return data.map(mapBoard)
}

export async function createBoard(payload: CreateBoardViewModel): Promise<Board> {
  const { data } = await apiClient.post<BoardResponse>('/api/board', payload)
  return mapBoard(data)
}
