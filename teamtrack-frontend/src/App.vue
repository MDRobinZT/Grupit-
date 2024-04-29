<template>

    <div v-if="route.name !== 'Avaleht'">
        <Navbar />
    </div>
    <router-view />
</template>

<script setup lang="ts">
    import {defineProps} from "vue";
    defineProps<{ isAthlete: Boolean, hasChosen: Boolean }>();

    import Navbar from "@/components/Navbar.vue";

    import { useRoute } from "vue-router";

    const route = useRoute();
    route.name
</script>


