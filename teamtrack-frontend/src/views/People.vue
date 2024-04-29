<template>  
  <PeopleList :title="title" :isAthlete="isAthlete"/>
</template>

<script setup lang="ts">

import PeopleList from "../components/PeopleList.vue"
  import {defineProps} from "vue";
  
  defineProps<{ title: String, isAthlete: Boolean }>();
</script>