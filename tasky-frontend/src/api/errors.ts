export interface ProblemDetails {
  title?: string | null
  detail?: string | null
  status?: number | null
  errors?: Record<string, string[]>
}

export class ApiError extends Error {
  constructor(
    public readonly status: number,
    message: string,
    public readonly fieldErrors?: Record<string, string[]>,
  ) {
    super(message)
    this.name = 'ApiError'
  }

  static fromResponse(status: number, body: unknown): ApiError {
    // pusty lub nie-JSON-owy body (np. 401 z middleware JWT) trafia tu jako string
    const problem = typeof body === 'object' && body !== null ? (body as ProblemDetails) : undefined

    return new ApiError(
      status,
      problem?.detail ?? problem?.title ?? `Request failed (${status})`,
      problem?.errors ? lowerFirstKeys(problem.errors) : undefined,
    )
  }
}

export const isApiError = (e: unknown): e is ApiError => e instanceof ApiError

// ASP.NET zwraca klucze walidacji np. jako "Name", a formularz ma "name"
function lowerFirstKeys(errors: Record<string, string[]>) {
  return Object.fromEntries(
    Object.entries(errors).map(([key, value]) => [
      key.charAt(0).toLowerCase() + key.slice(1),
      value,
    ]),
  )
}

type FetchResult<T> = { data?: T; error?: unknown; response: Response }

async function resolve<T>(request: Promise<FetchResult<T>>): Promise<FetchResult<T>> {
  let result: FetchResult<T>
  try {
    result = await request
  } catch (e) {
    if (e instanceof DOMException && e.name === 'AbortError') throw e // anulowanie nie jest błędem
    throw new ApiError(0, 'Brak połączenia z serwerem')
  }
  if (!result.response.ok) throw ApiError.fromResponse(result.response.status, result.error)
  return result
}

export async function unwrap<T>(request: Promise<FetchResult<T>>): Promise<T> {
  return (await resolve(request)).data as T
}

export async function unwrapVoid(request: Promise<FetchResult<unknown>>): Promise<void> {
  await resolve(request)
}

export function errorMessageFrom(e: unknown, fallback = 'Something went wrong.'): string {
  return isApiError(e) ? e.message : fallback
}
