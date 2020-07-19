<template>
  <div>
    <section class="section">
      <aside class="menu">
        <div class="row">
            <category-hashtag v-if="!isLoading" :title="'Danh mục'" :data="categories" :is-category=true />
        </div>
        <div class="row mt-5">
            <category-hashtag v-if="!isLoading" :title="'Hashtag'" :data="hashtags" :is-category=false />
        </div>
      </aside>
    </section>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import CategoryHashtag from "@/tin-tuc-app/components/CategoryHashtag/CategoryHashtag.vue";
import Category from "../../../../../WebsiteTinTuc.Admin/vue/src/store/entities/category";
import { Action, Getter } from 'vuex-class';
import Hashtag from "../../../../../WebsiteTinTuc.Admin/vue/src/store/entities/hashtag";
@Component({
  name: "MenuCategoryHashtag",
  components: { CategoryHashtag }
})
export default class MenuCategoryHashtag extends Vue {
    @Action("getAllCategory", { namespace: "CategoryModule" })
    public getCategory!: () => Promise<Category[]>;
    @Getter("categories", { namespace: "CategoryModule" })
    public categories!: Category[];
    @Action("getAllHashtag", { namespace: "HashtagModule" })
    public getHashtag!: () => Promise<Hashtag[]>;
    @Getter("categories", { namespace: "HashtagModule" })
    public hashtags!: Hashtag[];

    public isLoading = false;

    private async created() {
        this.isLoading = true;
        await this.getCategory();
        await this.getHashtag();
        this.isLoading = false;
    }
}
</script>

<style scoped>
</style>
