# DateStrip Day Picker — Design

## Purpose

`DateStrip.vue` currently renders a single hardcoded cell ("Today" / "5"). This
work turns it into a 7-day picker strip so the user can select a day and the
component reports that selection to its parent. Actual task filtering by the
selected date is out of scope (the `Task` type has no date field yet).

## Behavior

- **Range:** always `today ± 3 days` (7 cells). The window is fixed to the
  current date and never recenters based on which day is selected.
- **Default selection:** today, on mount.
- **Interaction:** clicking a cell selects it (updates internal state) and
  emits the newly selected date to the parent. No other behavior (e.g. task
  filtering) is wired up by this component.

## Component API

`src/components/list/DateStrip.vue`

- **Props:** none.
- **Emits:** `(e: 'select-date', date: Dayjs): void` — tuple-signature style,
  matching the existing `defineEmits` convention in `TaskComposer.vue`.
- **Internal state:** `selectedDate` ref (dayjs), defaulting to `dayjs()`
  (today) at mount.

## Date math

- `today = dayjs()`, computed once.
- `days = computed(...)`: array of 7 dayjs objects built as
  `today.add(i - 3, 'day')` for `i` in `0..6` → 3 days before today, today,
  3 days after today.
- `isToday(day)` / `isSelected(day)`: compare via `day.isSame(x, 'day')`.

## Template

- `<div v-for="day in days" :key="day.format('YYYY-MM-DD')">` — one cell per
  day, `@click="selectDate(day)"`.
- Each cell is a `<div>` with `cursor-pointer` (not a `<button>`), matching
  the existing DateStrip markup and this codebase's existing clickable-div
  pattern elsewhere.
- Cell contents, top to bottom:
  1. Label: `"Today"` for today's cell, otherwise `day.format('ddd')`
     (Mon/Tue/…).
  2. Day number: `day.format('D')`.
  3. Dot indicator (`size-1 rounded-pill`): rendered only when the cell is
     selected.

## Styling

- **Selected cell:** `bg-coral shadow-coral-sm`, white label/number text
  (`text-[#FFFFFFD9]` / `text-white`), dot visible. This matches the current
  DateStrip markup, which was effectively the "selected" style.
- **Unselected cell:** `bg-cream-50 border border-line-warm`,
  `text-ink`/`text-ink-400` text, no dot.

## Explicitly out of scope

- Wiring `select-date` into `ListView.vue` to actually filter tasks — blocked
  on `Task` not having a date field.
- Any navigation beyond the fixed ±3-day window (e.g. paging to other weeks).
- Keyboard/screen-reader accessibility for the day cells (deliberately kept
  as clickable `<div>`s, consistent with existing patterns in this codebase).
