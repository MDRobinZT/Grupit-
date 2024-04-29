<template>
    <div>
        <ul>
            <li>
                <router-link
                    to="/"
                    aria-current="page">
                    Avaleht
                </router-link>
            </li>
            <li>
                <router-link
                    v-if ="getPath() !== ('/athleteEvents')"
                    to="/newevent">
                    Lisa uus sündmus
                </router-link>
            </li>
            <li>
                <router-link
                    to="/peopleList"
                    aria-current="page">
                    Inimeste list
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script setup lang="ts">
    import {defineProps} from "vue";
    import { useRoute } from "vue-router";

    defineProps<{ title: String, isAthlete: Boolean }>();

    const route = useRoute();

    const getPath = () =>{
        const routc = route.fullPath;
        return routc;
    }
</script>

<style>
    ul {
        list-style-type: none;
        margin: 0;
        padding: 0;
        overflow: hidden;
        background-color: #111;
    }

    li {
        float: left;
    }

    li a 
    {
        display: block;
        color: #d2d2d2;
        text-align: center;
        padding: 14px 16px;
        text-decoration: none;
    }

    li a:hover {
        background-color: #000;
    }
</style>