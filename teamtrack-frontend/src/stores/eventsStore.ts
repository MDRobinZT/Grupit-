import { Event } from "@/models/event";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "@/models/api";

export const useEventsStore = defineStore('eventsStore', () => {
  const apiGetEvents = useApi<Event[]>('events');
  const events = ref<Event[]>([]);
  let allEvents: Event[] = [];

  const loadEvents = async () => {
    await apiGetEvents.request();

    if (apiGetEvents.response.value) {
      return apiGetEvents.response.value;
    }
    return [];
  };

  const load = async () => {
    allEvents = await loadEvents();
    events.value = allEvents;
  };
  const getEventById = (id: number) => {
    return allEvents.find((event) => event.id === id);
  };


  const addEvent = async (event: Event) => {
    const apiAddEvent = useApi<Event>('events', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(event),
    }); 
    
    await apiAddEvent.request();
    if (apiAddEvent.response.value) {
      load();      
    }
  };
  const updateEvent = async (event: Event) => {
    const apiUpdateEvent = useApi<Event>('events/' + event.id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(event),
    });

    await apiUpdateEvent.request();
    if (apiUpdateEvent.response.value) {
      load();
    }    
  };


  const deleteEvent = async (event: Event) => {
    const deleteEventRequest = useApiRawRequest(`events/${event.id}`, {
      method: 'DELETE',
    });

    const res = await deleteEventRequest();

    if (res.status === 204) {
      let id = events.value.indexOf(event);

      if (id !== -1) {
        events.value.splice(id, 1);
      }

      id = events.value.indexOf(event);

      if (id !== -1) {
        events.value.splice(id, 1);
      }
    }
  };

  return { events, load, getEventById, addEvent, updateEvent, deleteEvent };
});





