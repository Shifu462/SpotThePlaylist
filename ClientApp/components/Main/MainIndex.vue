<template>
    <div>
        <PlaylistTable @backgroundUrl="$emit('backgroundUrl', $event)" />
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import PlaylistTable from './PlaylistTable.vue';

@Component({
    components: {
        PlaylistTable,
    }
})
export default class MainIndex extends Vue {

}

</script>