<template>
  <div>
    <h1 class="font-bold">Lisa sündmuse</h1>

    <div class="min-h-full flex items-center justify-center py-0 px- sm:px-6 lg:px-8">
      <form class="max-w-md w-full space-y-9">
        <div class="mt-5 space-y-6">
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <label for="name">Sündmuse tüüp</label>
              <input
                id="name"
                name="name"
                v-model="event.type"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Nimetus"
              />
            </div>
            <br />
            <div>
              <label for="name">Kirjeldus</label>
              <input
                id="description"
                name="description"
                v-model="event.description"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Kirjeldus"
              />
            </div>
            <br />
            <div>
              <label for="location">Toimumise koht</label>
              <input
                id="location"
                name="location"
                v-model="event.location"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Toimumise koht"
              />
            </div>
            <br />
            <div>
              <label for="date">Kuupäev</label>
              <input
                id="date"
                name="date"
                v-model="event.date"
                type="date"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Kuupäev"
              />
            </div>
            <br />
            <div>
              <label for="time">Kellaaeg</label>
              <input
                id="time"
                name="time"
                v-model="time"
                type="time"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
              />
            </div>
            <br />
            <button
              @click.prevent="submitForm"
              class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              Lisa sündmust
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watchEffect } from 'vue';
import { useRouter } from 'vue-router';
import { useEventsStore } from "@/stores/eventsStore";
import { Event } from '@/models/event';
import { defineProps } from 'vue';

const props = defineProps({
  event: {
    type: Object as () => Event,
    required: true
  }
});

const localEvent = ref({...props.event});
const time = ref('');

const { addEvent } = useEventsStore();
const router = useRouter();

watchEffect(() => {
  localEvent.value = { ...props.event };
  // Split the ISO date to date and time
  const dateTime = props.event.date.split('T');
  localEvent.value.date = dateTime[0];
  time.value = dateTime[1]?.substring(0, 5) || '';
});

const submitForm = () => {
  // Combine the date and time to a full ISO string
  const isoDate = `${localEvent.value.date}T${time.value}:00.000Z`;
  const eventData = { ...localEvent.value, date: isoDate };

  addEvent(eventData);
  
  // Navigate to the events page after submitting
  router.push({ name: "Treeneri Sündmused" });
};
</script>

<style>
h1 {
  text-align: center;
  font-size: xx-large;
  font-family: Arial, Helvetica, sans-serif;
  font-weight: bold;
  padding: 30px;
}
</style>