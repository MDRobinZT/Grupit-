<template>
    <div
      class="min-h-screen bg-grey-50 py-12 px-4 sm:px-6 lg:px=8 text-black-300"
    >
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>
        <DataTable :value="people" v-if="people.length > 0">
          <Column field="Username" header="Kasutajanimi" />
          <Column field="Email" header="Email" />
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
        <div v-else>Inimesed puuduvad</div>
      </div>
      <div v-if="showPopup" class="popup">
        <div class="popup-inner">
          <h2>Person Details</h2>
          <ul>
            <li v-for="(value, key) in selectedPerson" :key="key">
              {{ key }}: {{ value }}
            </li>
          </ul>
          <button @click="showPopup = false" class="popupClose">X</button>
        </div>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { Person } from '@/models/Person';
  import { usePeopleStore } from "@/stores/peopleStore";
  import { storeToRefs } from "pinia";
  import { defineProps, onMounted, ref, watch } from "vue";
  import { useRoute } from "vue-router";
  
  
  const route = useRoute();
  
  watch(route, (to, from) => {
    // Check if the route has changed meaningfully before calling load.
    if (to.path !== from.path || to.query !== from.query) {
      peopleStore.load();
    }
  }, { deep: true });
  
  defineProps<{ title: String, isAthlete: Boolean }>();
  
  const showPopup = ref(false);
  const selectedPerson = ref({});
  
  const showDetails = (person: Person) => {
    selectedPerson.value = person;
    showPopup.value = true;
  };
  
  const peopleStore = usePeopleStore();
  const { people } = storeToRefs(peopleStore);
  
  onMounted(() => {
    peopleStore.load();
  });
  
  const remove = (person: Person) => {
    peopleStore.deleteperson(person);
  };
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
  }
  
  .popup-inner {
    background-color: #fff; /* White background for the popup */
    padding: 40px; /* More padding for a spacious look */
    border-radius: 15px; /* Rounded corners */
    box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2); /* Softer shadow for depth */
    position: relative; /* For absolute positioning of close button */
    width: 90%; /* Max width to support responsiveness */
    max-width: 500px; /* Maximum width of the popup */
    transition: transform 0.3s ease-in-out; /* Transform transition for a pop effect */
    transform: scale(1.05); /* Start slightly larger */
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
  
  