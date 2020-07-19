<template>
  <div>
    <p class="menu-label">{{title}}</p>
    <ul class="menu-list">
      <li v-for="item in data" :key="item">
        <router-link v-if="isCategory" :to="{ name: 'home', params: { id: item.categoryUrl } }">{{item.name}}</router-link>
        <router-link v-if="!isCategory" :to="{ name: 'home', params: { id: item.hashtagUrl } }">{{item.name}}</router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import Category from "../../../../../../WebsiteTinTuc.Admin/vue/src/store/entities/category";
@Component({
  name: "CategoryHashtag"
})
export default class CategoryHashtag extends Vue {
  @Prop({ type: String, default: "" }) public title!: string;
  @Prop({ type: Boolean, default: false }) public isCategory!: boolean;
  @Prop() public data!: any[];
}
</script>

<style scoped>
</style>
