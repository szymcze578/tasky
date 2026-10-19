import type { Dayjs } from 'dayjs'

export enum TaskStatus {
  ToDo = 'To do',
  InProgress = 'In progress',
  Postponed = 'Postponed',
  Reviewed = 'Reviewed',
  Completed = 'Completed',
  Canceled = 'Canceled',
}

export interface Task {
  status: TaskStatus
  name: string
  date?: Dayjs
}
