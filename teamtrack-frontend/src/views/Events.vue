<template>  
    <EventList :title="title" :isAthlete="isAthlete"/>
</template>
  
<script setup lang="ts">

import EventList from "../components/EventList.vue"
  import {defineProps} from "vue";
  
  defineProps<{ title: String, isAthlete: Boolean }>();
</script>