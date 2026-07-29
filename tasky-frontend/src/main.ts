import './assets/main.css'
import './config/dayjs.ts'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { bootstrap } from './api/services/bootstrap.ts'

const app = createApp(App)

app.use(createPinia())

await bootstrap()

app.use(router)
app.mount('#app')
