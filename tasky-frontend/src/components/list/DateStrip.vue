<template>
  <div class="flex flex-row gap-1.5">
    <div
      v-for="day in days"
      :key="day.format('YYYY-MM-DD')"
      data-testid="date-cell"
      class="flex flex-col w-12.5 rounded-xl cursor-pointer items-center gap-0.5 py-2"
      :class="isSelected(day) ? 'bg-coral shadow-coral-sm' : 'bg-cream-50 border border-line-warm'"
      @click="selectDate(day)"
    >
      <span
        class="font-display text-[10px] font-bold"
        :class="isSelected(day) ? 'text-[#FFFFFFD9]' : 'text-ink-400'"
      >
        {{ isToday(day) ? 'Today' : day.format('ddd') }}
      </span>
      <span
        class="font-display text-[16px] font-bold"
        :class="isSelected(day) ? 'text-white' : 'text-ink'"
      >
        {{ day.format('D') }}
      </span>
      <div v-if="isSelected(day)" class="size-1 rounded-pill bg-white" />
    </div>
  </div>
</template>

<script setup lang="ts">
  import type { Dayjs } from 'dayjs'
  import dayjs from 'dayjs'
  import { computed, ref } from 'vue'

  const emit = defineEmits<{
    (e: 'select-date', date: Dayjs): void
  }>()

  const today = dayjs()
  const selectedDate = ref<Dayjs>(today)
  const days = computed(() => Array.from({ length: 7 }, (_, i) => today.add(i - 3, 'day')))

  function isToday(day: Dayjs) {
    return day.isSame(today, 'day')
  }

  function isSelected(day: Dayjs) {
    return day.isSame(selectedDate.value, 'day')
  }

  function selectDate(day: Dayjs) {
    selectedDate.value = day
    emit('select-date', day)
  }
</script>
