<template>
    <div class="root">

        <AppNavbar class="navbar" />

        <AppContent class="content" @backgroundUrl="changeBackground" />

	</div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import AppContent from './AppContent.vue';
import AppNavbar from './AppNavbar.vue';

@Component({
    components: {
        AppContent,
        AppNavbar,
    }
})
export default class AppComponent extends Vue {
    changeBackground(url: string) {
        this.$el.setAttribute("style", `background: center url(${url}) no-repeat`);
    }
}

</script>

<style lang="less" scoped>
    .root {
        width: 100%;
        height: 100%;
    }

    .content {
        padding-top: 60px;
        width: 100%;
        height: 100%;
    }
</style>