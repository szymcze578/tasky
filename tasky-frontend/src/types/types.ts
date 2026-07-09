import type { Component } from 'vue'

export enum BoardType {
  List = 0,
  Kanban = 1,
}

export interface Board {
  id: string
  name: string
  type: BoardType
  editedAt: string
}

export interface Task {
  id: string
  title: string
  done: boolean
  date: string
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
