<template>
    <div class="root" :style="{
            backgroundImage: backgroundUrl && `url(${backgroundUrl})`
        }">

        <AppNavbar class="navbar" />

        <AppContent class="content" @backgroundUrl="backgroundUrl = $event" />

    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import AppContent from './AppContent.vue';
import AppNavbar from './AppNavbar.vue';

export default Vue.extend({
    components: {
        AppContent,
        AppNavbar,
    },
    data() {
        return {
            backgroundUrl: null,
        };
    }
});

</script>

<style lang="less" scoped>
    .root {
        width: 100%;
        height: 100%;
        // height: calc(100vh - 60px);

        background-position: center;
        background-repeat: no-repeat;
    }

    .navbar {
        position: fixed;
        top: 0;
    }
</style>