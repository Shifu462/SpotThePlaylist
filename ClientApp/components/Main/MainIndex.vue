<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import PlaylistTable from './PlaylistTable.vue';

export default Vue.extend({
    functional: true,
    render(h, ctx) {
        return h('div', { staticClass: 'root' }, [
            h(PlaylistTable, {
                on: {
                    backgroundUrl: url => (ctx.listeners['backgroundUrl'] as Function)(url),
                }
            }),
        ]);
    }
});

</script>

<style lang="less" scoped>
    .root {
        width: 100%;
        height: 100%;
    }
</style>