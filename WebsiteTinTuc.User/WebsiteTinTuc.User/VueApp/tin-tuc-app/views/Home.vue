<template>
  <div>
    <prominent-company />
    <list-company-post-home />
    <post :title="'Công việc mới nhất'" :posts="posts" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ProminentCompany from "@/tin-tuc-app/components/Home/ProminentCompany.vue";
import ListCompanyPostHome from "@/tin-tuc-app/components/Home/ListCompanyPostHome.vue";
import Post from "@/tin-tuc-app/components/Home/Post.vue";
import { Action, Getter } from "vuex-class";
import CompanyPostModel from "../store/interfaces/home";

@Component({
  name: "Home",
  components: {
    ProminentCompany,
    ListCompanyPostHome,
    Post
  },
})
export default class Home extends Vue {
  @Action("getTopNewPost", { namespace: "HomeModule" })
  private getTopNewPost!: (count: number) => void;
  @Getter("posts", { namespace: "HomeModule" })
  private posts!: CompanyPostModel[];

  private async created() {
    const count = 10;
    await this.getTopNewPost(count);
  }
}
</script>

<style lang="scss" scoped></style>
