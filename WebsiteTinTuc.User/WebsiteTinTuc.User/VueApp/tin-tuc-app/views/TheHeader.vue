<template>
    <div>
        <header-menu />
        <header-search class="mt-3" />
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import HeaderMenu from "@/tin-tuc-app/components/TheHeader/HeaderMenu.vue";
import HeaderSearch from "@/tin-tuc-app/components/TheHeader/HeaderSearch.vue";

@Component({
    name: 'TheHeader',
    components: { HeaderMenu, HeaderSearch }
})
export default class TheHeader extends Vue {
}
</script>

<style scoped>
</style>
