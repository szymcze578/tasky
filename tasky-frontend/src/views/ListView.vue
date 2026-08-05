<template>
  <div class="flex flex-col mx-auto w-full max-w-180 gap-4.5">
    <button
      class="flex w-fit items-center text-ink-500 gap-1.75 cursor-pointer hover:text-coral-600"
      @click="goBack()"
    >
      <ChevronLeft class="size-5" />
      <span class="font-display font-bold text-[14px]">All boards</span>
    </button>
    <div class="flex flex-row items-center gap-[15px]">
      <div
        class="flex h-13 w-13 flex-none items-center justify-center rounded-2xl bg-coral/10 text-coral"
      >
        <ListIcon :size="24" />
      </div>
      <div class="flex flex-col">
        <span class="textlist-title">Weekly groceries</span>
        <div class="mt-1.5 flex items-center gap-2.5">
          <span
            class="inline-flex items-center rounded-pill bg-blush px-2.5 py-0.75 text-[11.5px] font-bold text-ink-tag"
          >
            List
          </span>
          <span class="text-[13px] text-ink-400">7 tasks across all days</span>
        </div>
      </div>
    </div>
    <div class="flex gap-2">
      <TheInput
        v-model="newTaskName"
        class="flex flex-3/4"
        type="text"
        placeholder="Add new task..."
        :icon="CirclePlus"
      />
      <TheButton :type="ButtonType.Orange" text="Add" class="flex-1/4" />
    </div>
    <div class="flex flex-col gap-2">
      <TaskItem v-for="(_, index) in activeTasks" :key="index" v-model:task="activeTasks[index]!" />
      <TaskItem
        v-for="(_, index) in completedTasks"
        :key="index"
        v-model:task="completedTasks[index]!"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
  import ListIcon from '@/components/icons/ListIcon.vue'
  import TaskItem from '@/components/TaskItem.vue'
  import TheButton from '@/components/widgets/TheButton.vue'
  import TheInput from '@/components/widgets/TheInput.vue'
  import router from '@/router'
  import { ButtonType, TaskStatus, type Task } from '@/types/types'
  import { ChevronLeft, CirclePlus } from '@lucide/vue'
  import { computed, ref } from 'vue'

  //defineProps<{}>()
  const tasks = ref<Task[]>([
    {
      status: TaskStatus.ToDo,
      name: 'Review last week’s notes',
    },
    {
      status: TaskStatus.ToDo,
      name: 'Draft the project brief',
    },
    {
      status: TaskStatus.ToDo,
      name: 'Send update to the team',
    },
  ])

  const activeTasks = computed(() => tasks.value.filter((task) => task.status == TaskStatus.ToDo))

  const completedTasks = computed(() =>
    tasks.value.filter((task) => task.status == TaskStatus.Completed),
  )

  const newTaskName = ref('')

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
