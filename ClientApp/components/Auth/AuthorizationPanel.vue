<template>
    <section>

        <div class="login-form">
            <template v-if="!$store.getters.isLoggedIn">
                <div class="note-warn">У вас не сохранён токен.</div>

                <button class="spotify-button" @click="goToSpotifyAuth">
                    Получить
                </button>
            </template>
            <template v-else>
                <button class="spotify-button" @click="removeToken">
                    Выход
                </button>
                <router-link class="spotify-button" to="/">
                    К музыке
                </router-link>
            </template>
        </div>

    </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Spotify from '../../utils/Spotify';

@Component({})
export default class AuthorizationPanel extends Vue {
    removeToken() {
        this.$store.commit('removeToken');
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
