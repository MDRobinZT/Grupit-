import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import EventsVue from '@/views/Events.vue';
import AddEventVue from '@/views/AddEvent.vue';
import UpdateEventVue from '@/views/UpdateEvent.vue'
import HomePageViewVue from '@/views/HomePageView.vue';
import PeopleList from '@/components/PeopleList.vue';


const routes: Array<RouteRecordRaw> = [
  { 
    path: '/',
    name: 'Avaleht',
    component: HomePageViewVue,
  },
  {
    path: '/athleteEvents',
    name: 'Sportlase Sündmused',
    component: EventsVue,
    props: { title: 'Sündmused', isAthlete: true},
  },
  {
    path: '/coachEvents',
    name: 'Treeneri Sündmused',
    component: EventsVue,
    props: { title: 'Sündmused', isAthlete: false},
  },
  {
    path: '/newevent',
    name: 'Lisa uus sündmus',
    component: AddEventVue,
  },
  {
    path: '/update/:id',
    name: 'Uuenda sündmuse',
    component: UpdateEventVue,
  },
  {
    path: '/peopleList',
    name: 'Inimeste list',
    component: PeopleList,
    props: { title: 'Sündmused', isAthlete: false},
  },
  {
    path: '/addPerson',
    name: 'Lisa uue inimese',
    component: PeopleList,
  },
  {
    path: '/updatePerson/:id',
    name: 'Uuenda inimese',
    component: PeopleList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
