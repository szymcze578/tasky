<template>
  <AuthCard>
    <template #title>Where should we bill you?</template>
    <template #subtitle>Add your address to finish setting up your account.</template>
    <template #content>
      <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
        <AuthTextField
          v-model="street"
          label="Street address"
          autocomplete="street-address"
          placeholder="Enter street name"
          :icon="House"
        />
        <div class="flex gap-3.5">
          <div class="flex-[1.4] min-w-0">
            <AuthTextField
              v-model="city"
              label="City"
              autocomplete="address-level2"
              placeholder="City you live in"
              :icon="Building"
            />
          </div>
          <div class="flex-1 min-w-0">
            <AuthTextField
              v-model="postal"
              label="Postal code"
              autocomplete="postal-code"
              placeholder="00-000"
            />
          </div>
        </div>
        <AuthSelectField
          v-model="country"
          label="Country"
          placeholder="Select a country…"
          autocomplete="country"
          :options="countryOptions"
          :icon="Globe"
        />
        <button
          type="submit"
          :disabled="!isValid"
          class="mt-2 rounded-xl bg-ink px-3.75 py-2.75 text-[14.5px] font-medium text-cream-50 transition-colors disabled:cursor-not-allowed disabled:opacity-50"
        >
          Save address
        </button>
      </form>
    </template>
    <template #footer>
      <RouterLink to="/" class="font-bold text-coral-600 no-underline">Skip for now </RouterLink>
    </template>
  </AuthCard>
</template>

<script setup lang="ts">
  import AuthCard from '@/components/auth/AuthCard.vue'
  import AuthTextField from '@/components/auth/AuthTextField.vue'
  import AuthSelectField from '@/components/auth/AuthSelectField.vue'
  import { computed, ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { Building, Globe, House } from '@lucide/vue'
  import { countryOptions } from '@/constants/countries'
  import { RouterLink } from 'vue-router'
  import { useUserStore } from '@/stores/userStore'
  import { errorMessageFrom, isApiError } from '@/api/errors'

  const router = useRouter()
  const userStore = useUserStore()

  const street = ref('')
  const city = ref('')
  const postal = ref('')
  const country = ref('')
  const isSubmitting = ref(false)
  const errorMessage = ref('')

  const isValid = computed(
    () =>
      street.value.trim() !== '' &&
      city.value.trim() !== '' &&
      postal.value.trim() !== '' &&
      country.value !== '',
  )

  async function handleSubmit() {
    if (!isValid.value) return

    errorMessage.value = ''
    isSubmitting.value = true
    try {
      await userStore.saveAddress({
        street: street.value.trim(),
        city: city.value.trim(),
        postalCode: postal.value.trim(),
        country: country.value.trim(),
      })

      router.push('/')
    } catch (e) {
      errorMessage.value =
        isApiError(e) && e.status === 401 ? 'Invalid address.' : errorMessageFrom(e)
    } finally {
      isSubmitting.value = false
    }
  }
</script>
