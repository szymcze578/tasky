<template>
  <div class="flex gap-2">
    <TheInput
      v-model="newTaskName"
      class="flex flex-3/4"
      type="text"
      placeholder="Add new task..."
      :icon="CirclePlus"
    />
    <TheButton :type="ButtonType.Orange" text="Add" class="flex-1/4" @on-click="addTask" />
  </div>
</template>

<script setup lang="ts">
  import { ButtonType } from '@/types/types'
  import TheButton from '../widgets/TheButton.vue'
  import { CirclePlus } from '@lucide/vue'
  import { ref } from 'vue'
  import TheInput from '../widgets/TheInput.vue'

  const emit = defineEmits<{
    (e: 'addTask', taskName: string): void
  }>()

  const newTaskName = ref('')

  function addTask() {
    emit('addTask', newTaskName.value)
    newTaskName.value = ''
  }
</script>
