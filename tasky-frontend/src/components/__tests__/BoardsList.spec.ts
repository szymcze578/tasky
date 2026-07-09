import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import BoardsList from '../BoardsList.vue'
import { BoardType, type Board } from '@/types/types'

describe('BoardsList', () => {
  it('renders boards passed in via the boards prop', () => {
    const boards: Board[] = [
      { id: '1', name: 'Marketing Launch', type: BoardType.List, editedAt: '2026-07-01' },
    ]

    const wrapper = mount(BoardsList, { props: { boards } })

    expect(wrapper.text()).toContain('Marketing Launch')
  })

  it('reflects updates to the boards prop', async () => {
    const wrapper = mount(BoardsList, { props: { boards: [] } })
    expect(wrapper.text()).toContain('No boards yet')

    await wrapper.setProps({
      boards: [{ id: '2', name: 'Product Roadmap', type: BoardType.Kanban, editedAt: '2026-07-08' }],
    })

    expect(wrapper.text()).toContain('Product Roadmap')
  })
})
