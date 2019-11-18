<template>
    <section>

        <div v-if="token">
            Отлично! Ждём...
        </div>

    </section>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({})
export default class AuthSuccess extends Vue {
    @Prop(String)
    readonly token!: string;

    created() {
        if (!this.token) {
            this.$router.push({
                path: '/auth'
            });
            return;
        }

        this.$store.commit('updateToken', this.token);
    }
}

</script>

<style lang="less" scoped>
    section {
        font-size: 18px;
    }
</style>
