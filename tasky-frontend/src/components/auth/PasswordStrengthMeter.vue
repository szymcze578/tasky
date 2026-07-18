<template>
  <div class="mt-2.5 flex gap-1.5">
    <div
      v-for="i in 4"
      :key="i"
      class="h-1.25 flex-1 rounded-pill transition-colors"
      :style="{ background: i - 1 < current ? colors[current] : '#EFE2DA' }"
    />
  </div>
  <div class="mt-1.5 text-[12px] text-ink-400">{{ label }}</div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps({
  password: { type: String, default: '' },
})

const current = computed(() => score(props.password))

const label = computed(() =>
  props.password
    ? `${labels[current.value]} password`
    : 'Use 8+ characters with a mix of letters, numbers & symbols',
)

function score(p: string) {
  let s = 0
  if (p.length >= 8) s++
  if (/[A-Z]/.test(p) && /[a-z]/.test(p)) s++
  if (/\d/.test(p)) s++
  if (/[^A-Za-z0-9]/.test(p)) s++
  return p.length === 0 ? 0 : s
}

const colors = ['#EBDCD1', '#E0745A', '#F2A65A', '#D9B54A', '#6FB08A']
const labels = ['', 'Weak', 'Fair', 'Good', 'Strong']
</script>
