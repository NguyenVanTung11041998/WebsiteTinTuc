<template>
    <div>
        <div class="container">
            <h1 class="title">
                {{ pageTitle }}
            </h1>
            <h2 class="subtitle">
                Template is created using Vue, Vue router, Vuex, TypeScript and Bulma.
            </h2>
            <template-info-about></template-info-about>
            <template-info-stack></template-info-stack>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'

import TemplateInfoAbout from '@/tin-tuc-app/components/TemplateInfo/TemplateInfoAbout.vue'
import TemplateInfoStack from '@/tin-tuc-app/components/TemplateInfo/TemplateInfoStack.vue'

@Component({
    name: 'TemplateInfo',
    components: {
        TemplateInfoAbout,
        TemplateInfoStack
    }
})
export default class Home extends Vue {
    pageTitle: string = 'Tin tức 24h';
}
</script>

<style lang="scss" scoped>
    .title {
        text-align: center;
    }
</style>
