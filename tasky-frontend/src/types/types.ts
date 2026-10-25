import type { BoardType } from '@/api/types'
import type { Component } from 'vue'

export enum BoardTypes {
  List = 'List',
  Kanban = 'Kanban',
}

export interface Board {
  id: string
  name: string
  type: BoardType
  editedAt: string
}

export interface KanbanCard {
  id: string
  title: string
  tag?: string
  tagColor?: string
}

export interface Column {
  id: string
  title: string
  accent: string
  cards: KanbanCard[]
}

export interface User {
  name: string
  plan: string
  initials: string
  userId: string
}

export interface NavItem {
  id: string
  label: string
  icon?: Component
  count?: number
}

export enum ButtonType {
  White,
  Orange,
}
