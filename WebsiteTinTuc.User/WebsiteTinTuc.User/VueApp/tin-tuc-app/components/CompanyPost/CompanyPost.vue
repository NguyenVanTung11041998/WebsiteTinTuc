<template>
  <div class="col-lg-12">
    <div class="mt-2">
      <div class="row">
        <div class="col-lg-12">
          <title-hot :title="title" />
        </div>
      </div>
      <div class="row mt-4">
        <div
          :class="classCss"
          v-for="item in data"
          :key="item.companyId"
          class="link-post"
        >
          <router-link
            :to="{ name: postRouteName, params: { id: item.postUrl } }"
          >
            <company-post-item :company-post="item" />
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "../Home/TitleHot.vue";
import VueHorizontalList from "vue-horizontal-list";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import CompanyPostModel from "../../store/interfaces/home";
import CompanyPostItem from "./CompanyPostItem.vue";

@Component({
  name: "CompanyPost",
  components: {
    TitleHot,
    VueHorizontalList,
    CompanyPostItem,
  },
})
export default class CompanyPost extends Vue {
  @Prop() data!: CompanyPostModel[];
  @Prop({ type: String, default: "" }) title!: string;
  @Prop({ type: String, default: "" }) classCss!: string;
  private postRouteName = RouteName.Post;
}
</script>

<style lang="scss" scoped>
.link-post {
  a {
    text-decoration: none;
  }
}
</style>
