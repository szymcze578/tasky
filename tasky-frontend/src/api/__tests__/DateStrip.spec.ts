import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import dayjs from 'dayjs'
import DateStrip from '@/components/list/DateStrip.vue'

describe('DateStrip', () => {
  it('renders 7 day cells spanning today ±3 days', () => {
    const wrapper = mount(DateStrip)
    const today = dayjs()

    for (let i = -3; i <= 3; i++) {
      expect(wrapper.text()).toContain(today.add(i, 'day').format('D'))
    }
  })

  it('labels today\'s cell "Today" and others by weekday', () => {
    const wrapper = mount(DateStrip)
    const today = dayjs()

    expect(wrapper.text()).toContain('Today')
    expect(wrapper.text()).toContain(today.subtract(1, 'day').format('ddd'))
  })

  it('selects today by default', () => {
    const wrapper = mount(DateStrip)
    const today = dayjs()
    const cells = wrapper.findAll('[data-testid="date-cell"]')
    const selected = cells.find((c) => c.text().includes(today.format('D')))

    expect(selected?.classes()).toContain('bg-coral')
  })

  it('emits select-date with the clicked day and updates selection styling', async () => {
    const wrapper = mount(DateStrip)
    const cells = wrapper.findAll('[data-testid="date-cell"]')

    await cells[0]!.trigger('click')

    expect(wrapper.emitted('select-date')).toBeTruthy()
    expect(cells[0]!.classes()).toContain('bg-coral')
  })
})
