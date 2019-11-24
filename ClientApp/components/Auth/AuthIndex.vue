<template>
    <div class="root">
        <AuthorizationPanel />
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import AuthorizationPanel from './AuthorizationPanel.vue';

@Component({
    components: {
        AuthorizationPanel,
    }
})
export default class AuthIndex extends Vue {

}

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