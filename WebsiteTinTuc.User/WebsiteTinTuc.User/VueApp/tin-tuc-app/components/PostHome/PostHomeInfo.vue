<template>
  <div class="row mt-2">
    <div class="col-lg-12">
      <div class="row">
        <div class="col-lg-9">
          <div class="row">
            <header-thumbnail
              :company-name="post.name"
              :thumbnail="post.thumbnail"
              :full-name-company="post.fullNameCompany"
            />
          </div>
          <div class="row mt-3">
            <post-content
              :content="post.content"
              :title="post.title"
              :location="post.location"
              :money-type="post.moneyType"
              :min-money="post.minMoney"
              :max-money="post.maxMoney"
              :level-name="post.levelName"
              :time-experience="post.timeExperience"
              :experience-type="post.experienceType"
              :end-date="post.endDate"
              :job-type="post.jobType"
              :time-create-new-job="post.timeCreateNewJob"
              :hashtag-posts="post.hashtagPosts"
            />
          </div>
        </div>
        <div class="col-lg-3">
          <company-info-treatment
            :website="post.website"
            :location-description="post.locationDescription"
            :treatment="post.treatment"
            :hashtags="post.hashtags"
            :nationality="post.nationality"
            :company-jobs="post.companyJobs"
            :max-scale="post.maxScale"
            :min-scale="post.minScale"
          />
        </div>
      </div>
    </div>
    <div class="col-lg-12 mt-3" v-if="posts && posts.length > 0">
      <div class="col-lg-12">
        <post-paging :posts="posts" @get-data="GetData" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import HeaderThumbnail from "../CompanyPostHomeInfo/HeaderThumbnail.vue";
import CompanyHome, {
  CompanyHomeInfoRequest,
  PostCompanyHome,
  PostHomeInfoDto,
} from "../../store/interfaces/company-home";
import PostContent from "../CompanyPostHomeInfo/PostContent.vue";
import PostPaging from "../CompanyPostHomeInfo/PostPaging.vue";
import CompanyInfoTreatment from "../CompanyPostHomeInfo/CompanyInfoTreatment.vue";

@Component({
  name: "PostHomeInfo",
  components: {
    HeaderThumbnail,
    PostContent,
    PostPaging,
    CompanyInfoTreatment,
  },
})
export default class PostHomeInfo extends Vue {
  @Prop({ type: Object, default: null }) private readonly post!: PostHomeInfoDto;
  @Action("getPostOfCompanyPaging", { namespace: "CompanyHomeModule" })
  private getPostOfCompanyPaging!: (
    params: CompanyHomeInfoRequest
  ) => Promise<void>;
  @Getter("posts", { namespace: "CompanyHomeModule" })
  private posts!: PostCompanyHome[];

  private async GetData(page: number) {
    let params = {
      url: this.post.companyUrl,
      currentPage: page,
      pageSize: 10,
      searchText: "",
      postId: this.post.id
    } as CompanyHomeInfoRequest;
    await this.getPostOfCompanyPaging(params);
  }
}
</script>

<style lang="scss" scoped></style>
