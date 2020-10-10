<template>
  <div class="row mt-2">
    <div class="col-lg-12">
      <div class="row">
        <div class="col-lg-8">
          <div class="row">
            <header-thumbnail
              :company-name="company.name"
              :thumbnail="company.thumbnail"
              :full-name-company="company.fullNameCompany"
            />
          </div>
          <div class="row mt-3">
            <company-content
              :images="company.images"
              :description="company.description"
            />
          </div>
        </div>
        <div class="col-lg-4">
          <company-info-treatment
            :website="company.website"
            :location-description="company.locationDescription"
            :treatment="company.treatment"
            :hashtags="company.hashtags"
            :nationality="company.nationality"
            :company-jobs="company.companyJobs"
            :max-scale="company.maxScale"
            :min-scale="company.minScale"
          />
        </div>
      </div>
    </div>
    <div class="col-lg-12 mt-3">
      <div class="col-lg-12">
        <post-paging :posts="posts" @get-data="GetData" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "../Home/TitleHot.vue";
import { Action, Getter } from "vuex-class";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import HeaderThumbnail from "../CompanyPostHomeInfo/HeaderThumbnail.vue";
import CompanyHome, {
  CompanyHomeInfoRequest,
  PostCompanyHome,
} from "../../store/interfaces/company-home";
import CompanyContent from "../CompanyPostHomeInfo/CompanyContent.vue";
import PostPaging from "../CompanyPostHomeInfo/PostPaging.vue";
import CompanyInfoTreatment from "../CompanyPostHomeInfo/CompanyInfoTreatment.vue";

@Component({
  name: "CompanyHomeInfo",
  components: {
    HeaderThumbnail,
    CompanyContent,
    PostPaging,
    CompanyInfoTreatment,
  },
})
export default class CompanyHomeInfo extends Vue {
  @Prop({ type: Object, default: null }) private readonly company!: CompanyHome;
  @Action("getPostOfCompanyPaging", { namespace: "CompanyHomeModule" })
  private getPostOfCompanyPaging!: (
    params: CompanyHomeInfoRequest
  ) => Promise<void>;
  @Getter("posts", { namespace: "CompanyHomeModule" })
  private posts!: PostCompanyHome[];

  private async GetData(page: number) {
    let params = {
      url: this.$route.params.id,
      currentPage: page,
      pageSize: 10,
      searchText: "",
    } as CompanyHomeInfoRequest;
    await this.getPostOfCompanyPaging(params);
  }
}
</script>

<style lang="scss" scoped></style>
