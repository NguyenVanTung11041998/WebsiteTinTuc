<template>
    <div>
        <header-menu />
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import HeaderMenu from "@/tin-tuc-app/components/TheHeader/HeaderMenu.vue";

@Component({
    name: 'TheHeader',
    components: { HeaderMenu }
})
export default class TheHeader extends Vue {
}
</script>

<style scoped>
</style>
