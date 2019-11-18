<template>
    <section>

        <div v-if="currentlyPlaying">
            {{ currentlyPlaying.artists.map(x => x.name).join(', ') }}:
            {{ currentlyPlaying.name }}
        </div>

        <table class="playlist">

            <tr>
                <th>Название</th>
                <th>Исполнитель</th>
                <th>Альбом</th>
            </tr>

            <tr class="song-row" v-for="song in songsLoaded" :key="song.author + song.name">
                <td class="author">
                    {{ song.author }}
                </td>
                <td class="name">
                    {{ song.name }}
                </td>
                <td class="duration">
                    {{ song.duration }}
                </td>
            </tr>

        </table>

    </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Spotify from "../utils/Spotify";

interface Song {
    author: string;
    name: string;
    duration: string;
}

@Component({})
export default class PlaylistTable extends Vue {
    songsLoaded: Song[] = [
        {
            author: 'Skeleton Hands',
            name: 'Shadows',
            duration: '6:10',
        },
        {
            author: 'Skeleton Hands',
            name: 'Oxygen',
            duration: '5:38',
        },
        {
            author: 'Skeleton Hands',
            name: 'Ravage',
            duration: '4:49',
        },
        {
            author: 'Кобыла и Трупоглазые Жабы Искали Цезию, Нашли Поздно Утром Свистящего Хна',
            name: 'Распад Петросовета',
            duration: '5:25',
        },
    ];

    spotify = new Spotify(this.$store.getters.token);

    currentlyPlaying: any = null;

    async created() {
        this.currentlyPlaying = await this.spotify.getCurrentlyPlaying();
    }
}

</script>

<style lang="less">
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

                &.duration {
                    max-width: 30px;
                }
            }

            &:hover {
                background-color: rgba(255, 255, 255, 0.1);
            }
        }
    }

</style>
