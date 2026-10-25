<template>
  <div class="flex flex-col mx-auto w-full max-w-180 gap-4.5">
    <button
      class="flex w-fit items-center text-ink-500 gap-1.75 cursor-pointer hover:text-coral-600"
      @click="goBack()"
    >
      <ChevronLeft class="size-5" />
      <span class="font-display font-bold text-[14px]">All boards</span>
    </button>
    <div class="flex flex-row items-center gap-3.75">
      <div
        class="flex h-13 w-13 flex-none items-center justify-center rounded-2xl bg-coral/10 text-coral"
      >
        <ListIcon :size="24" />
      </div>
      <div class="flex flex-col">
        <span class="textlist-title">{{ board?.name }}</span>
        <div class="mt-1.5 flex items-center gap-2.5">
          <span
            class="inline-flex items-center rounded-pill bg-blush px-2.5 py-0.75 text-[11.5px] font-bold text-ink-tag"
          >
            List
          </span>

          <span v-if="tasks.length" class="text-[13px] text-ink-400">
            {{ tasks.length }} tasks across all days
          </span>
          <span v-else class="text-[13px] text-ink-400"> No tasks yet - add them! </span>
        </div>
      </div>
    </div>
    <DateStrip></DateStrip>
    <TaskComposer @add-task="(value) => addNewTask(value)" />
    <div v-if="tasks.length" class="flex flex-col gap-5">
      <div>
        <div v-if="activeTasks.length" class="text-[12px] font-bold text-ink-300 ml-0.5 mb-3">
          TO DO - {{ activeTasks.length }}
        </div>
        <TaskItem
          v-for="task in activeTasks"
          :key="task.taskId"
          :task="task"
          @toggle="toggleTask(task)"
        />
      </div>
      <div>
        <div v-if="completedTasks" class="text-[12px] font-bold text-ink-300 ml-0.5 mb-3">
          COMPLETED - {{ activeTasks.length }}
        </div>
        <TaskItem
          v-for="task in completedTasks"
          :key="task.taskId"
          :task="task"
          @toggle="toggleTask(task)"
        />
      </div>
    </div>
    <div
      v-else
      class="rounded-3xl border-[1.5px] border-dashed border-line-input bg-cream-50 px-5 py-10 text-center text-[14px] text-ink-400"
    >
      No tasks yet — create your first one above.
    </div>
  </div>
</template>

<script setup lang="ts">
  import { isApiError } from '@/api/errors'
  import { tasksApi } from '@/api/task'
  import type { Board, Task } from '@/api/types'
  import ListIcon from '@/components/icons/ListIcon.vue'
  import DateStrip from '@/components/list/DateStrip.vue'
  import TaskComposer from '@/components/list/TaskComposer.vue'
  import TaskItem from '@/components/list/TaskItem.vue'
  import router from '@/router'
  import { useBoardStore } from '@/stores/boardStore'
  import { TaskStatus } from '@/types/taskTypes'
  import { ChevronLeft } from '@lucide/vue'
  import { computed, ref, watch } from 'vue'

  const props = defineProps<{ boardId: string }>()

  const boardStore = useBoardStore()

  const board = ref<Board | null>(null)
  const tasks = ref<Task[]>([])
  const isLoading = ref(true)
  const loadError = ref<string | null>(null)
  const actionError = ref<string | null>(null)

  const activeTasks = computed(() => tasks.value.filter((t) => t.status === 'ToDo'))
  const completedTasks = computed(() => tasks.value.filter((t) => t.status === 'Completed'))

  const messageFrom = (e: unknown, fallback: string) => (isApiError(e) ? e.message : fallback)

  async function load(boardId: string) {
    isLoading.value = true
    loadError.value = null
    try {
      const [loadedBoard, loadedTasks] = await Promise.all([
        boardStore.ensureBoard(boardId),
        tasksApi.getTasks(boardId),
      ])
      board.value = loadedBoard
      tasks.value = loadedTasks
    } catch (e) {
      if (isApiError(e) && e.status === 404) {
        router.replace('/')
        return
      }
      loadError.value = messageFrom(e, 'Failed to load board')
    } finally {
      isLoading.value = false
    }
  }

  watch(() => props.boardId, load, { immediate: true })

  async function addNewTask(title: string) {
    const trimmed = title.trim()
    if (!trimmed) return

    actionError.value = null
    try {
      console.log('creating task')
      const created = await tasksApi.addTask(props.boardId, { title: trimmed })
      tasks.value.push(created)
    } catch (e) {
      actionError.value = messageFrom(e, 'Failed to add task')
    }
  }

  function replaceTask(updated: Task) {
    tasks.value = tasks.value.map((t) => (t.taskId === updated.taskId ? updated : t))
  }

  async function toggleTask(task: Task) {
    const previous = task.status
    const next = previous === TaskStatus.Completed ? 'ToDo' : 'Completed'

    actionError.value = null
    replaceTask({ ...task, status: next })
    try {
      if (!task.taskId) return
      replaceTask(await tasksApi.changeTaskStatus(props.boardId, task.taskId, { status: next }))
    } catch (e) {
      replaceTask({ ...task, status: previous }) // wycofanie
      actionError.value = messageFrom(e, 'Failed to update task')
    }
  }

  function goBack() {
    router.push('/')
  }
</script>

<style>
  .textlist-title {
    font-family: var(--font-display);
    font-weight: var(--font-weight-bold);
    font-size: 28px;
    line-height: 1.1;
  }
</style>
