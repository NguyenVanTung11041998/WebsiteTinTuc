<template>
  <div class="title-category-hashtag">
    <p class="menu-label">{{title}}</p>
    <ul class="menu-list">
      <li v-for="item in data" :key="item.id">
        <router-link
          v-if="isCategory"
          :to="{ name: 'category', params: { id: item.categoryUrl } }"
        >{{item.name}}</router-link>
        <router-link
          v-if="!isCategory"
          :to="{ name: 'hashtag', params: { id: item.hashtagUrl } }"
        >{{item.name}}</router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
@Component({
  name: "CategoryHashtag",
})
export default class CategoryHashtag extends Vue {
  @Prop({ type: String, default: "" }) public title!: string;
  @Prop({ type: Boolean, default: false }) public isCategory!: boolean;
  @Prop() public data!: any[];
}
</script>

<style lang="scss" scoped>
.title-category-hashtag {
  text-align: center;
  width: 100%;
  a {
    text-decoration: darkorchid;
  }
}
</style>
