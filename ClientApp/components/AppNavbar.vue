<template>
    <nav class="nav-main">
        <router-link class="nav-main-item" to="/auth">
            Auth
        </router-link>

        <router-link class="nav-main-item" to="/" v-if="hasToken">
            <font-awesome-icon :icon="['fab', 'spotify']" class="icon" />
            <span>Find something new!</span>
        </router-link>
    </nav>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";

@Component({})
export default class AppNavbar extends Vue {
    get hasToken(): boolean {
        return !!this.$store.getters.token;
    }
}

</script>

<style lang="less" scoped>
    .nav-main {
        width: 100%;

        z-index: 100;
        padding-left: 40px;

        background-color: var(--spotify-bar-black);
        border-bottom: 1px solid var(--spotify-inactive);
    }

    .nav-main-item {
        display: inline-block;

        color: white;
        font-size: 16px;

        padding: 20px 10px;

        .icon {
            margin-right: 5px;
        }

        &:hover {
            background: rgba(255, 255, 255, 0.2);
        }
    }
</style>