<template>
    <div class="root">
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

<style lang="less" scoped>
    .root {
        width: 100%;
        height: 100%;
    }
</style>