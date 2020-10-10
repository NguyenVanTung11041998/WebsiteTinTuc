<template>
  <div class="row mt-3">
    <div class="col-lg-12">
      <title-hot :title="title" />
    </div>
    <div class="row mt-3 post-content">
      <div class="col-lg-12" v-for="item in posts" :key="item.postId">
        <post-company-info :post="item" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "../Home/TitleHot.vue";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import PostCompanyInfo from "./PostCompanyInfo.vue";
import { PostCompanyHome } from "../../store/interfaces/company-home";

@Component({
  name: "ListPostCompanyInfo",
  components: {
    TitleHot,
    PostCompanyInfo
  },
})
export default class ListPostCompanyInfo extends Vue {
  @Prop({ type: String, default: "" }) private readonly title!: string;
  @Prop() private readonly posts!: PostCompanyHome[];
}
</script>

<style lang="scss" scoped>
  .post-content {
    width: 100%;
  }
</style>
