<template>
  <div class="row" v-if="!isLoading">
    <div class="company-top" v-if="companyPostProminents.length > 0">
      <company-post
        class="company-post"
        :data="companyPostProminents"
        :class-css="'col-lg-3'"
        :title="'Các công ty nổi bật'"
      />
      <div class="col-lg-12 mt-3">
        <paginate
          :page-count="pagePaginate"
          :page-range="3"
          :margin-pages="2"
          :prev-text="prevPaginate"
          :next-text="nextPaginate"
          :container-class="'pagination'"
          :page-class="'page-item'"
          :click-handler="getDataCompanyPostProminent"
          v-model="currentPage"
        />
      </div>
    </div>
    <div class="company-bottom mt-3" v-if="companyPosts.length > 0">
      <company-post
        class="company-post"
        :data="companyPosts"
        :class-css="'col-lg-4'"
        :title="'Các công ty khác'"
      />
      <div class="col-lg-12 mt-3">
        <paginate
          :page-count="pagePaginateButtom"
          :page-range="3"
          :margin-pages="2"
          :prev-text="prevPaginate"
          :next-text="nextPaginate"
          :container-class="'pagination'"
          :page-class="'page-item'"
          :click-handler="getDataCompanyPost"
          v-model="currentPageBottom"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import TitleHot from "./TitleHot.vue";
import VueHorizontalList from "vue-horizontal-list";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import CompanyPostModel, { HomeFilter } from "../../store/interfaces/home";
import CompanyPost from "@/tin-tuc-app/components/CompanyPost/CompanyPost.vue";
import Paginate from "vuejs-paginate";

@Component({
  name: "ListCompanyPostHome",
  components: {
    TitleHot,
    VueHorizontalList,
    CompanyPost,
    Paginate,
  },
})
export default class ListCompanyPostHome extends Vue {
  @Action("getAllCompanyPostPaging", { namespace: "HomeModule" })
  private getAllCompanyPostPaging!: (filter: HomeFilter) => void;
  @Getter("companyPostProminents", { namespace: "HomeModule" })
  private companyPostProminents!: CompanyPostModel[];
  @Getter("companyPosts", { namespace: "HomeModule" })
  private companyPosts!: CompanyPostModel[];
  @Getter("pagePaginate", { namespace: "HomeModule" })
  private pagePaginate!: number;
  @Getter("pagePaginateButtom", { namespace: "HomeModule" })
  private pagePaginateButtom!: number;

  private isLoading = true;
  private companyRouteName = RouteName.Company;
  private postRouteName = RouteName.Post;
  private currentPage = 1;
  private pageSize = 20;
  private nextPaginate = '<i class="fas fa-angle-right"></i>';
  private prevPaginate = '<i class="fas fa-angle-left"></i>';

  private currentPageBottom = 1;
  private pageSizeBottom = 15;

  private async created() {
    await this.getDataCompanyPostProminent();
    await this.getDataCompanyPost();
  }

  private async getDataCompanyPostProminent() {
    const filter = {
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      isHot: true,
    } as HomeFilter;
    this.isLoading = true;
    await this.getAllCompanyPostPaging(filter);
    this.isLoading = false;
  }

  private async getDataCompanyPost() {
    const filter = {
      currentPage: this.currentPageBottom,
      pageSize: this.pageSizeBottom,
      isHot: false,
    } as HomeFilter;
    this.isLoading = true;
    await this.getAllCompanyPostPaging(filter);
    this.isLoading = false;
  }

  private getLinkPath(file: IObjectFile): string {
    return file ? Util.getLinkPath(file.path) : "#";
  }
}
</script>

<style lang="scss">
.company-top, .company-bottom {
  width: 100%;
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
}
</style>
