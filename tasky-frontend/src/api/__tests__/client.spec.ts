import { describe, it, expect } from 'vitest'
import { AxiosError, AxiosHeaders } from 'axios'
import { normalizeError } from '../client'

describe('normalizeError', () => {
  it('extracts status and message from an AxiosError response', () => {
    const error = new AxiosError(
      'Request failed',
      'ERR_BAD_REQUEST',
      undefined,
      undefined,
      {
        status: 404,
        statusText: 'Not Found',
        headers: new AxiosHeaders(),
        config: { headers: new AxiosHeaders() },
        data: { message: 'Board not found' },
      },
    )

    expect(normalizeError(error)).toEqual({ status: 404, message: 'Board not found' })
  })

  it('falls back to the AxiosError message when the response has no message', () => {
    const error = new AxiosError('Network Error')

    expect(normalizeError(error)).toEqual({ status: 0, message: 'Network Error' })
  })

  it('returns a generic error for non-Axios errors', () => {
    expect(normalizeError('boom')).toEqual({ status: 0, message: 'Unknown error' })
  })
})
