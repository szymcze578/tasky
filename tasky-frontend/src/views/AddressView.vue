<template>
  <AuthCard title="Where should we bill you?">
    <template #subtitle>Add your address to finish setting up your account.</template>
    <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
      <AuthTextField
        v-model="street"
        label="Street address"
        autocomplete="street-address"
        placeholder="123 Maple Street, Apt 4B"
        :icon="House"
      />
      <div class="flex gap-3.5">
        <div class="flex-[1.4] min-w-0">
          <AuthTextField
            v-model="city"
            label="City"
            autocomplete="address-level2"
            placeholder="Warsaw"
            :icon="Building"
          />
        </div>
        <div class="flex-1 min-w-0">
          <AuthTextField
            v-model="postal"
            label="Postal code"
            autocomplete="postal-code"
            placeholder="00-001"
          />
        </div>
      </div>
      <AuthSelectField
        v-model="country"
        label="Country"
        placeholder="Select a country…"
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
    <template #footer>
      <a href="#" class="font-bold text-coral-600 no-underline">Skip for now</a>
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

  const router = useRouter()

  const street = ref('')
  const city = ref('')
  const postal = ref('')
  const country = ref('')

  const countryOptions = [
    { value: 'PL', label: 'Poland' },
    { value: 'DE', label: 'Germany' },
    { value: 'FR', label: 'France' },
    { value: 'GB', label: 'United Kingdom' },
    { value: 'US', label: 'United States' },
    { value: 'ES', label: 'Spain' },
    { value: 'IT', label: 'Italy' },
  ]

  const isValid = computed(
    () =>
      street.value.trim() !== '' &&
      city.value.trim() !== '' &&
      postal.value.trim() !== '' &&
      country.value !== '',
  )

  function handleSubmit() {
    if (!isValid.value) return
    router.push('/')
  }
</script>
