<template>
  <div
    class="min-h-screen bg-grey-50 py-12 px-4 sm:px-6 lg:px=8 text-black-300"
  >
    <div class="text-center">
      <h1 class="font-bold">{{ title }}</h1>
      <DataTable :value="events" v-if="events.length > 0">
        <Column field="type" header="Nimetus" />
        <Column field="location" header="Asukoht" />
        <Column header="Kuupäev">
        <template #body="{ data }">
            {{  formatDateTime(data.date).date }}
        </template>
        </Column>
        <Column header="Kellaaeg">
        <template #body="{ data }">
            {{ formatDateTime(data.date).time }}
        </template>
        </Column>
        <Column v-if="!isAthlete">
          <template #body="{ data }">
            <router-link
                class="border bg-blue-400 text-blue-900 py-0 px-2 mx-2 border-red-900 font-bold"
                :to="'update/' + data.id"
              >
                ⭮
              </router-link>

            <button
              class="border bg-red-400 text-white py-0 px-2 border-red-900 font-bold"
              @click="remove(data)"
            >
              Delete
            </button>

            <button
              class="border bg-green-400 text-white py-0 px-2 border-green-900 font-bold"
              @click="showDetails(data)"
            >
              Details
            </button>
          </template>
        </Column>
      </DataTable>
      <div v-else>Sündmused puuduvad</div>
    </div>
    <div v-if="showPopup" class="popup">
        <div v-if="showPopup" class="popup">
            <div class="popup-inner">
                <h2>Event Details</h2>
                <ul>
                    <li>
                        <strong>Nimetus:</strong> {{ selectedEvent.type }}
                    </li>
                    <li>
                        <strong>Asukoht:</strong> {{ selectedEvent.location }}
                    </li>
                    <li>
                        <strong>Kuupäev:</strong> {{ formatDateTime(selectedEvent.date).date }}
                    </li>
                    <li>
                        <strong>Kellaaeg:</strong> {{ formatDateTime(selectedEvent.date).time }}
                    </li>
                </ul>
                <button @click="showPopup = false" class="popupClose">X</button>
            </div>
        </div>

    </div>
  </div>
</template>

<script setup lang="ts">
import { Event } from '@/models/event';
import { useEventsStore } from "@/stores/eventsStore";
import { storeToRefs } from "pinia";
import { defineProps, onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";


const route = useRoute();

watch(route, (to, from) => {
  // Check if the route has changed meaningfully before calling load.
  if (to.path !== from.path || to.query !== from.query) {
    eventsStore.load();
  }
}, { deep: true });

defineProps<{ title: String, isAthlete: Boolean }>();

const showPopup = ref(false);
const selectedEvent = ref({});

const showDetails = (event: Event) => {
  selectedEvent.value = event;
  showPopup.value = true;
};

const eventsStore = useEventsStore();
const { events } = storeToRefs(eventsStore);

onMounted(() => {
  eventsStore.load();
});

const remove = (event: Event) => {
  eventsStore.deleteEvent(event);
};

const formatDateTime = (isoString: string) => {
    const dateTime = new Date(isoString);
    const timeZone = 'UTC';
    const optionsDate: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit', timeZone: timeZone };
    const optionsTime: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit', hour12: false, timeZone: timeZone };
    return {
        date: dateTime.toLocaleDateString(undefined, optionsDate),
        time: dateTime.toLocaleTimeString(undefined, optionsTime)
    };
}
</script>

<style>
    /* General styles */
    .min-h-screen {
        background-color: #f8fafc; /* A softer shade of grey */
        color: #111827; /* Dark grey for better readability */
    }

    /* Typography and spacing */
    h1 {
        padding: 1rem 0; /* More vertical padding */
        font-size: 2.25rem; /* Larger size for title */
        line-height: 2.5rem;
        margin-bottom: 2rem; /* Space below the title */
    }

    /* Buttons and links */
    button, .router-link {
        padding: 0.5rem 1rem; /* Increased padding for larger clickable area */
        margin: 0 0.25rem; /* Slight separation between buttons */
        border: none; /* Remove default borders */
        font-weight: 600; /* Semi-bold font for better legibility */
        transition: background-color 0.3s; /* Smooth background transition for hover effect */
    }

        button:hover, .router-link:hover {
            background-color: darken(bg-color, 10%); /* Darken button on hover for feedback */
        }

    .popup {
        /* ... existing styles ... */
        display: flex; /* Keep this to align the popup */
        align-items: center;
        justify-content: center;
        transition: opacity 0.3s ease-in-out; /* Smooth fade transition */
        z-index: 100; /* High z-index to ensure visibility above other content */
        background-color: #f8fafc;
    }

    .popup-inner {
        /* ... existing styles ... */
        background-color: #f8fafc;
        padding: 40px;
        border-radius: 15px;
        box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
        position: relative;
        width: 90%;
        max-width: 500px;
        transition: transform 0.3s ease-in-out;
        transform: scale(1.05);
        text-align: center; /* Keskendame sisu horisontaalselt */
    }

        .popup-inner h2 {
            margin-top: 0; /* Remove margin at the top of the h2 */
            margin-bottom: 1rem; /* Space below the title */
            color: #333; /* Dark color for the title */
            font-size: 1.75rem; /* Larger size for the title */
        }

        .popup-inner ul {
            list-style: none; /* Remove list styles */
            padding: 0; /* Remove padding */
            margin: 0; /* Remove margins for a clean look */
            background-color: #f8fafc;
            display: flex;
            flex-direction: column; /* Change flex direction to column */
            align-items: center; /* Center content horizontally */
            justify-content: center; /* Center content vertically */
            gap: 1rem; /* Add gap between list items */
            transition: transform 0.3s ease-in-out;
            transform: scale(1.05);
        }

            .popup-inner ul li {
                padding: 0.5rem 0; /* Padding for each list item */
                border-bottom: 1px solid #eee; /* Separator for items */
            }

                .popup-inner ul li:last-child {
                    border-bottom: none; /* No border for the last item */
                }

    .popupClose {
        position: absolute;
        top: 10px;
        right: 10px;
        width: 30px; /* Width and height should be the same for a circle */
        height: 30px;
        line-height: 30px; /* Line height equal to height to center the text vertically */
        text-align: center; /* Center the text horizontally */
        border-radius: 50%; /* Perfect circle */
        font-size: 16px; /* Adjust the font size as needed */
        transition: background-color 0.2s, transform 0.2s;
        cursor: pointer;
        display: flex; /* Use flexbox to center content */
        align-items: center; /* Center vertically */
        justify-content: center; /* Center horizontally */
        padding: 0; /* Remove padding to prevent misalignment */
    }

        .popupClose:hover,
        .popupClose:focus {
            background-color: #e11d48; /* Darker shade for hover effect */
            color: #fff;
            outline: none; /* Remove outline on focus for a cleaner look */
        }

    /* Showing and hiding the popup */
    .popup-enter-active, .popup-leave-active {
        transition: opacity 0.3s ease-in-out;
    }

    .popup-enter, .popup-leave-to /* .popup-leave-active below version 2.1.8 */ {
        opacity: 0;
        transform: scale(1);
    }

    /* DataTable styling */
    .DataTable {
        /* Assuming you have a custom class to target */
        width: 100%; /* Full width */
        border-collapse: collapse; /* Collapsed borders for a clean look */
        margin-top: 1rem; /* Space above the table */
    }

    .Column {
        /* Assuming custom classnames for Column */
        text-align: left; /* Align text to the left for readability */
        padding: 0.75rem 1rem; /* Padding within cells */
    }

    /* Media queries for responsiveness */
    @media (min-width: 768px) {
        .min-h-screen {
            padding: 2rem; /* More padding on larger screens */
        }
    }
</style>