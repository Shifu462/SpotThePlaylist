<template>
    <section class="playlist-root">

        <section>
            <button @click="refreshAll" class="spotify-button" :class="{ inactive: isRefreshing }">
                <font-awesome-icon icon="sync" class="icon" />
            </button>

            <button @click="getAndSavePlaylist" class="spotify-button" :class="{ inactive: isRefreshing || isSaved }">
                <font-awesome-icon icon="save" class="icon" />
            </button>
        </section>

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
    isRefreshing = false;
    isSaved = true;

    recommendedTracks: Song[] = [];

    readonly spotify = new Spotify(this.$store.getters.token);

    get displayedSongs(): Song[] {
        if (!this.recommendedTracks) return [];

        return this.recommendedTracks.slice(0, 20);
    }

    async created() {
        await this.refreshAll();
    }

    async refreshAll() {
        if (this.isRefreshing) return;

        const refreshBackground = async () => {
            const backgroundUrl = await this.spotify.getRandomBackgroundUrl();
            this.$emit('backgroundUrl', backgroundUrl);
        }

        const refreshRecommendations = async () => {
            this.isRefreshing = true;

            this.recommendedTracks = [];
            this.recommendedTracks = await this.spotify.getNewPlaylist();

            this.isRefreshing = false;
            this.isSaved = false;
        }

        refreshBackground();
        await refreshRecommendations();
    }

    async getAndSavePlaylist() {
        if (this.isRefreshing || this.isSaved) return;

        await this.spotify.createPlaylist(this.recommendedTracks.map(x => x.Id));
        this.isSaved = true;
    }
}

</script>

<style lang="less" scoped>

    .playlist-root {
        padding: 30px;

        display: flex;
        flex-direction: column;

        justify-content: center;
    }

    .playlist {
        position: relative;
        table-layout: fixed;

        min-width: 500px;
        max-width: 900px;

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

    .spotify-button .icon {
        padding: 0 5px;
    }

</style>
