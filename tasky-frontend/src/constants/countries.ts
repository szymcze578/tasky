const EU = [
  'AT',
  'BE',
  'BG',
  'HR',
  'CY',
  'CZ',
  'DK',
  'EE',
  'FI',
  'FR',
  'DE',
  'GR',
  'HU',
  'IE',
  'IT',
  'LV',
  'LT',
  'LU',
  'MT',
  'NL',
  'PL',
  'PT',
  'RO',
  'SK',
  'SI',
  'ES',
  'SE',
]

const AMERICAS = [
  // Ameryka Północna
  'CA',
  'US',
  'MX',
  // Ameryka Środkowa
  'BZ',
  'CR',
  'SV',
  'GT',
  'HN',
  'NI',
  'PA',
  // Karaiby
  'AG',
  'BS',
  'BB',
  'CU',
  'DM',
  'DO',
  'GD',
  'HT',
  'JM',
  'KN',
  'LC',
  'VC',
  'TT',
  // Ameryka Południowa
  'AR',
  'BO',
  'BR',
  'CL',
  'CO',
  'EC',
  'GY',
  'PY',
  'PE',
  'SR',
  'UY',
  'VE',
]

const OTHER = ['GB']

const regionNames = new Intl.DisplayNames(['en'], { type: 'region' })

export const countryOptions = [...new Set([...EU, ...AMERICAS, ...OTHER])]
  .map((code) => ({ value: code, label: regionNames.of(code) ?? code }))
  .sort((a, b) => a.label.localeCompare(b.label))
