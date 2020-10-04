<template>
  <div class="row mt-3">
    <div class="col-lg-12">
      <title-hot :title="title" />
    </div>
    <div class="row mt-3 post-content">
      <div class="col-lg-12" v-for="item in posts" :key="item.postId">
        <post-item :post="item" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "./TitleHot.vue";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import CompanyPostModel from "../../store/interfaces/home";
import PostItem from "../CompanyPost/PostItem.vue";

@Component({
  name: "Post",
  components: {
    TitleHot,
    PostItem
  },
})
export default class Post extends Vue {
  @Prop({ type: String, default: "" }) private readonly title!: string;
  @Prop() private readonly posts!: CompanyPostModel[];
}
</script>

<style lang="scss" scoped>
  .post-content {
    width: 100%;
  }
</style>
