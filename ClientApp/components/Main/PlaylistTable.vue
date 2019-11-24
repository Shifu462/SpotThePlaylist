<template>
    <section class="playlist-root">

        <button @click="refreshAll" class="spotify-button">
            <font-awesome-icon
                icon="sync"
                class="refresh-button"/>
        </button>

        <table class="playlist">

            <tr>
                <th>Название</th>
                <th>Исполнитель</th>
                <th>Альбом</th>
            </tr>

            <tr class="song-row" v-for="song in displayedSongs" :key="song.Author + song.Name">
                <td class="author">
                    {{ song.Author }}
                </td>
                <td class="name">
                    {{ song.Name }}
                </td>
                <td class="album">
                    {{ song.Album }}
                </td>
            </tr>

        </table>

        <button @click="getAndSavePlaylist" class="spotify-button">
            save playlist
        </button>


    </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Spotify from "../../utils/Spotify";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

interface Song {
    Id: string;
    Author: string;
    Name: string;
    Album: string;
}

@Component({})
export default class PlaylistTable extends Vue {
    loadedSongs: Song[] = [];

    spotify = new Spotify(this.$store.getters.token);

    get displayedSongs(): Song[] {
        if (!this.loadedSongs) return [];

        return this.loadedSongs.slice(0, 20);
    }

    created() {
        this.refreshAll();
    }

    async refreshAll() {
        this.refreshBackground();
        this.refreshRecommendations();
    }

    async refreshBackground() {
        const backgroundUrl = await this.spotify.getRandomBackgroundUrl();
        this.$emit('backgroundUrl', backgroundUrl);
    }

    async refreshRecommendations() {
        this.loadedSongs = [];
        this.loadedSongs = await this.spotify.getNewPlaylist();
    }

    async getAndSavePlaylist() {
        await this.spotify.createPlaylist(this.loadedSongs.map(x => x.Id));
    }
}

</script>

<style lang="less">

    .playlist-root {
        -webkit-backdrop-filter: blur(10px);
        backdrop-filter: blur(10px);
        background-color: rgba(0, 0, 0, 0.35);

        padding: 30px;

        display: flex;
        flex-direction: column;

        justify-content: center;
    }

    .playlist {
        table-layout: fixed;

        min-width: 500px;

        th {
            text-transform: uppercase;
            text-align: start;
            padding: 15px 0;
            font-size: 12px;
            color: var(--spotify-inactive);
        }

        .song-row {
            td {
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;

                padding: 5px 15px;
                max-width: 350px;
            }

            &:hover {
                background-color: rgba(255, 255, 255, 0.1);
            }
        }
    }

    .refresh-button {
        padding: 0 5px;
    }

</style>
