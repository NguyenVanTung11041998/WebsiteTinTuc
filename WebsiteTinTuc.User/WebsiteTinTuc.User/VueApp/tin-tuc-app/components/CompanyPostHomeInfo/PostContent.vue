<template>
  <div class="col-lg-12">
    <div class="row">
      <div class="col-lg-8">
        <h1 class="title-post mt-2">Công việc: {{ title }}</h1>
        <p class="mt-2" v-html="content" />
      </div>
      <div class="col-lg-4 mt-2">
        <button class="btn-recruitment btn btn-warning">Ứng tuyển ngay</button>
        <p class="text-align-recruitment mt-3">Hoặc</p>
        <button class="btn-recruitment-light btn btn-light mt-3">
          Ứng tuyển không dùng CV
        </button>
        <p class="text-align-recruitment mt-3">{{ getTime() }}</p>
        <p class="text-title mt-4">Địa điểm</p>
        <p>{{ location }}</p>
        <p class="text-title mt-4">Năm kinh nghiệm</p>
        <p>{{ getExperience() }}</p>
        <p class="text-title mt-4">Cấp độ</p>
        <p>{{ levelName }}</p>
        <p class="text-title mt-4">Loại công việc</p>
        <p>{{ getJobType() }}</p>
        <div v-if="hashtagPosts && hashtagPosts.length > 0">
          <h3 class="text-title mt-4">Kỹ năng</h3>
          <button
            v-for="item in hashtagPosts"
            :key="item.id"
            class="btn btn-light mt-2 mr-2"
          >
            <router-link
              :to="{ name: routeHashtag, params: { id: item.hashtagUrl } }"
            >
              {{ item.name }}
            </router-link>
          </button>
        </div>
        <div v-if="endDate">
            <p class="text-title mt-4">Hạn nộp hồ sơ</p>
            <p>{{ `${new Date(endDate).toLocaleDateString()}` }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import CONSTANT_VARIABLE from "../../constants/constant-variable";
import RouteName from "../../constants/route-name";
import { ExperienceType } from "../../store/enums/experience-type";
import { JobType } from "../../store/enums/job-type";
import { MoneyType } from "../../store/enums/money-type";
import { HashtagCompanyHome } from "../../store/interfaces/company-home";

@Component({
  name: "PostContent",
  components: {},
})
export default class PostContent extends Vue {
  @Prop({ type: String, default: "" }) private readonly content!: string;
  @Prop({ type: String, default: "" }) private readonly title!: string;
  @Prop({ type: String, default: "" }) private readonly location!: string;
  @Prop({ type: Number, default: MoneyType.NoNegotiable })
  private readonly moneyType!: MoneyType;
  @Prop({ type: Number, default: null }) private readonly minMoney!: number;
  @Prop({ type: Number, default: null }) private readonly maxMoney!: number;
  @Prop({ type: Number, default: 0 })
  private readonly timeCreateNewJob!: number;
  @Prop({ type: String, default: "" }) private readonly levelName!: string;
  @Prop({ type: Number, default: null })
  private readonly timeExperience!: number;
  @Prop({ type: Number, default: ExperienceType.NoExperience })
  private readonly experienceType!: ExperienceType;
  @Prop() private readonly endDate!: Date;
  @Prop({ type: Number, default: JobType.PartTime })
  private readonly jobType!: JobType;
  @Prop() private readonly hashtagPosts!: HashtagCompanyHome[];
  private readonly routeHashtag = RouteName.Hashtag;

  private getTime(): string {
    return this.timeCreateNewJob > CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY
      ? `${Math.floor(
          this.timeCreateNewJob / CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY
        )} ngày trước`
      : `${this.timeCreateNewJob} giờ trước`;
  }

  private getExperience(): string {
    if (this.experienceType == ExperienceType.NoExperience) {
      return "Không yêu cầu kinh nghiệm";
    } else if (this.experienceType == ExperienceType.Month) {
      return `${this.timeExperience} tháng kinh nghiệm`;
    }
    return `${this.timeExperience} năm kinh nghiệm`;
  }

  private getJobType(): string {
    return this.jobType == JobType.PartTime ? "Part time" : "Full time";
  }
}
</script>

<style lang="scss" scoped>
.title-post {
  font-size: 20px;
  font-weight: bold;
  color: #e44d27;
}

.btn-recruitment {
  background: #d34127 !important;
  color: white;
  width: 100%;
  height: 50px;
}

.btn-recruitment-light {
  width: 100%;
  height: 50px;
}

.btn-recruitment-light:hover {
  color: #d34127;
}

.text-align-recruitment {
  text-align: center;
}

.text-title {
  font-size: 14px;
  font-weight: bold;
}
</style>
