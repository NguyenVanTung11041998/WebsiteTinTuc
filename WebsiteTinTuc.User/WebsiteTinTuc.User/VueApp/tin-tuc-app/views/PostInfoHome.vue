<template>
  <div>
    <post-home-info v-if="!isLoading" :post="post" />
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import ProminentCompany from "@/tin-tuc-app/components/Home/ProminentCompany.vue";
import ListCompanyPostHome from "@/tin-tuc-app/components/Home/ListCompanyPostHome.vue";
import Post from "@/tin-tuc-app/components/Home/Post.vue";
import { Action, Getter } from "vuex-class";
import CompanyPostModel from "../store/interfaces/home";
import CompanyHome, {
  CompanyHomeInfoRequest,
} from "../store/interfaces/company-home";
import PostHomeInfo from "@/tin-tuc-app/components/PostHome/PostHomeInfo.vue";
import { Guid } from "guid-typescript";

@Component({
  name: "PostInfoHome",
  components: { PostHomeInfo },
})
export default class PostInfoHome extends Vue {
  @Action("getPostByUrl", { namespace: "CompanyHomeModule" })
  private getPostByUrl!: (params: CompanyHomeInfoRequest) => void;
  @Getter("post", { namespace: "CompanyHomeModule" })
  private post!: CompanyHome;
  private isLoading = false;

  private get newId(): string {
    return this.$route.params.id;
  }

  private async created() {
    await this.getData();
  }

  @Watch("newId")
  private async getData() {
    this.isLoading = true;
    let params = {
      url: this.$route.params.id,
      currentPage: 1,
      pageSize: 10,
      searchText: "",
    } as CompanyHomeInfoRequest;
    await this.getPostByUrl(params);
    this.isLoading = false;
  }
}
</script>

<style lang="scss" scoped></style>
