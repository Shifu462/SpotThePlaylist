<template>
    <section>

        <div class="login-form" v-if="!$store.getters.isLoggedIn">
            <div class="note-warn">У вас отсутствует печенье.</div>

            <button class="spotify-button" @click="goToSpotifyAuth">
                Получить печенье
            </button>

        </div>

    </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Spotify from '../utils/Spotify';

@Component({})
export default class AuthorizationPanel extends Vue {
    created() {
        if (this.$store.getters.isLoggedIn) {
            this.$router.push({
                path: '/playlist',
            });
            return;
        }
    }

    goToSpotifyAuth() {
        window.open(Spotify.AuthLink, '_blank');
    }
}

</script>

<style lang="less" scoped>
    .login-form {
        > *:not(:first-child) {
            margin-top: 10px;
        }
    }
</style>
