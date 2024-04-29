import { createApp } from 'vue';
import App from './App.vue';
import { createPinia } from 'pinia';
import 'virtual:windi.css';
import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/saga-blue/theme.css'; 
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';
import router from './router/router';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import { setApiUrl } from './models/api';

const getRuntimeConf = async () => {
  const runtimeConf = await fetch('/config/runtime-config.json');
  return await runtimeConf.json();
};

const app = createApp(App);

getRuntimeConf().then((json) => {
  setApiUrl(json.API_URL);

  app.use(createPinia());
  app.use(PrimeVue);
  app.use(router);

  app.component('DataTable', DataTable);
  app.component('Column', Column);

  app.mount('#app');
});

