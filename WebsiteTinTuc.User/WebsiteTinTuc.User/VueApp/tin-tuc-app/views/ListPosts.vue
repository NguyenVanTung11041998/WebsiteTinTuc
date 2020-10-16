<template>
  <div>
    <post :title="title" :posts="posts" />
    <div class="col-lg-12 mt-3" v-if="page > 1">
      <paginate
        :page-count="page"
        :page-range="3"
        :margin-pages="2"
        :prev-text="prevPaginate"
        :next-text="nextPaginate"
        :container-class="'pagination'"
        :page-class="'page-item'"
        :click-handler="getData"
        v-model="currentPage"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import CompanyPostModel, { PostFilter } from "../store/interfaces/home";
import Post from "../components/Home/Post.vue";
import Paginate from "vuejs-paginate";

@Component({
  name: "ListPosts",
  components: { Post, Paginate },
})
export default class ListPosts extends Vue {
  @Action("getPostPaging", { namespace: "HomeModule" })
  private getPostPaging!: (filter: PostFilter) => void;
  @Getter("pagePaginate", { namespace: "HomeModule" })
  private page!: number;
  @Getter("posts", { namespace: "HomeModule" })
  private posts!: CompanyPostModel[];
  @Getter("place", { namespace: "HomeModule" })
  private place!: number;

  private title = "Danh sách công việc";
  private currentPage = 1;
  private pageSize = 30;
  private searchText = "";
  private nextPaginate = '<i class="fas fa-angle-right"></i>';
  private prevPaginate = '<i class="fas fa-angle-left"></i>';

  private async created() {
    await this.getData();
  }

  get newId() {
    return this.$route.params.id;
  }

  get newPlace() {
    return this.place;
  }

  @Watch("newPlace")
  private async getDataPost() {
    await this.getData();
  }

  @Watch("newId")
  private async getData() {
    this.searchText = this.$route.params.id;
    let location = "";
    switch (this.place) {
      case 1: {
        location = "Hà Nội"
        break;
      }
      case 2: {
        location = "Đà Nẵng"
        break;
      }
      case 3: {
        location = "Hồ Chí Minh"
        break;
      }
    }
    let params = {
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.searchText,
      location: location
    } as PostFilter;
    await this.getPostPaging(params);
  }
}
</script>

<style lang="scss">
.pagination {
  justify-content: center;
  background: #fff;
  padding: 15px;
  border-radius: 4px;
  display: flex;
  li {
    margin: 0 1px;
    a {
      width: 35px;
      height: 35px;
      line-height: 35px;
      text-align: center;
      display: inline-block;
    }
    &:hover,
    &.active {
      a {
        background-color: #fa7973;
        color: #fff;
        text-decoration: underline;
      }
    }
  }
}
</style>
