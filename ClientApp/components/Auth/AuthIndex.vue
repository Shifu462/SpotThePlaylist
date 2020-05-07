<script lang="ts">
import Vue from 'vue';
import AuthorizationPanel from './AuthorizationPanel.vue';

export default Vue.extend({
    functional: true,
    render(h) {
        return h('div', { staticClass: 'root' }, [
            h(AuthorizationPanel),
        ]);
    }
});

</script>

<style lang="less" scoped>
    .root {
        width: 100%;
        height: 100%;

        display: flex;
        justify-content: center;
        align-items: center;
    }
</style>