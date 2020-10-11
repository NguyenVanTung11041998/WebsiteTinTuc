<template>
  <div>
    <list-post-company-info :title="title" :posts="posts" />
    <div class="col-lg-12 mt-3">
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
import { Vue, Component, Watch, Prop } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import ListPostCompanyInfo from "./ListPostCompanyInfo.vue";
import Paginate from "vuejs-paginate";
import { PostCompanyHome } from "../../store/interfaces/company-home";

@Component({
  name: "PostPaging",
  components: { ListPostCompanyInfo, Paginate },
})
export default class PostPaging extends Vue {
  @Getter("pagePaginate", { namespace: "CompanyHomeModule" })
  private page!: number;
  @Prop() private readonly posts!: PostCompanyHome[];

  private title = "Danh sách công việc";
  private currentPage = 1;
  private searchText = "";
  private nextPaginate = '<i class="fas fa-angle-right"></i>';
  private prevPaginate = '<i class="fas fa-angle-left"></i>';

  private async created() {
    await this.getData();
  }

  private async getData() {
    this.$emit("get-data", this.currentPage)
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
