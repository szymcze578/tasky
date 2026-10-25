import { api } from './client'
import { unwrap, unwrapVoid } from './errors'
import type { Board, CreateBoardRequest, UpdateBoardRequest } from './types'

function getBoards(): Promise<Board[]> {
  return unwrap(api.GET('/api/boards'))
}

function getBoard(boardId: string): Promise<Board> {
  return unwrap(api.GET('/api/boards/{boardId}', { params: { path: { boardId } } }))
}

function createBoard(body: CreateBoardRequest): Promise<Board> {
  return unwrap(api.POST('/api/boards', { body }))
}

function updateBoard(boardId: string, body: UpdateBoardRequest): Promise<Board> {
  return unwrap(api.PUT('/api/boards/{boardId}', { params: { path: { boardId } }, body }))
}

function deleteBoard(boardId: string) {
  return unwrapVoid(api.DELETE('/api/boards/{boardId}', { params: { path: { boardId } } }))
}

export const boardsApi = {
  getAll: getBoards,
  get: getBoard,
  create: createBoard,
  update: updateBoard,
  remove: deleteBoard,
}
