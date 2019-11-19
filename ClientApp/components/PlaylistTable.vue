<template>
    <section class="playlist-root">

        <table class="playlist">

            <tr>
                <th>Название</th>
                <th>Исполнитель</th>
                <th>Альбом</th>
            </tr>

            <tr class="song-row" v-for="song in songs" :key="song.Author + song.Name">
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

    </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Spotify from "../utils/Spotify";

interface Song {
    Author: string;
    Name: string;
    Album: string;
}

@Component({})
export default class PlaylistTable extends Vue {
    loadedSongs: Song[] = [];

    spotify = new Spotify(this.$store.getters.token);

    get songs(): Song[] {
        if (!this.loadedSongs) return [];

        return this.loadedSongs.slice(0, 20);
    }

    async created() {
        const backgroundUrl = await this.spotify.getRandomBackgroundUrl();

        if (backgroundUrl) this.$emit('backgroundUrl', backgroundUrl);

        this.loadedSongs = await this.spotify.getRecentTracks();
    }
}

</script>

<style lang="less">

    .playlist-root {
        -webkit-backdrop-filter: blur(10px);
        backdrop-filter: blur(10px);
        background-color: rgba(0, 0, 0, 0.35); 

        padding: 30px;
    }

    .playlist {
        table-layout: fixed;

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

</style>
