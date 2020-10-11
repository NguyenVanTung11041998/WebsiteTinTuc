<template>
  <div class="col-lg-12">
    <h3 class="website-text">Website</h3>
    <p class="website-link mt-2">
      <a :href="website" target="_blank">{{ website }}</a>
    </p>
    <h3 class="website-text mt-2">Địa điểm</h3>
    <p class="mt-2">{{ locationDescription }}</p>
    <div v-if="minScale > 0 || maxScale > 0">
      <h3 class="website-text mt-2">Số lượng nhân viên</h3>
      <p class="mt-2">
        {{
          minScale > 0 && maxScale > 0
            ? `${minScale} - ${maxScale}`
            : !minScale
            ? maxScale
            : minScale
        }}
      </p>
    </div>
    <div v-if="companyJobs && companyJobs.length > 0">
      <h3 class="website-text mt-2">Ngành nghề</h3>
      <button
        v-for="item in companyJobs"
        :key="item.id"
        class="btn btn-light mt-2 mr-2"
      >
        <router-link
          :to="{ name: routeBranchJob, params: { id: item.companyJobUrl } }"
        >
          {{ item.name }}
        </router-link>
      </button>
    </div>
    <div v-if="hashtags && hashtags.length > 0">
      <h3 class="website-text mt-2">Kỹ thuật/Công nghệ</h3>
      <button
        v-for="item in hashtags"
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
    <h3 class="website-text mt-2">Quốc tịch công ty</h3>
    <p class="mt-2">
      <img
        class="image-nationality"
        :src="getLinkPath()"
        :alt="nationality.name"
      />
      <span class="ml-1">{{ nationality.name }}</span>
    </p>
    <h3 class="website-text mt-2">Đãi ngộ</h3>
    <p class="mt-2" v-html="treatment" />
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch, Prop } from "vue-property-decorator";
import RouteName from "../../constants/route-name";
import Util from "../../constants/util";

import {
  CompanyJobHome,
  HashtagCompanyHome,
  NationalityCompany,
} from "../../store/interfaces/company-home";

@Component({
  name: "CompanyInfoTreatment",
  components: {},
})
export default class CompanyInfoTreatment extends Vue {
  @Prop({ type: String, default: "" }) private readonly website!: string;
  @Prop({ type: String, default: "" })
  private readonly locationDescription!: string;
  @Prop({ type: String, default: "" }) private readonly treatment!: string;
  @Prop({ type: Number, default: null }) private readonly maxScale!: number;
  @Prop({ type: Number, default: null }) private readonly minScale!: number;
  @Prop() private readonly companyJobs!: CompanyJobHome[];
  @Prop({ type: Object, default: "" })
  private readonly nationality!: NationalityCompany;
  @Prop() private readonly hashtags!: HashtagCompanyHome[];

  private readonly routeBranchJob = RouteName.BranchJob;
  private readonly routeHashtag = RouteName.Hashtag;

  private getLinkPath(): string {
    return this.nationality ? Util.getLinkPath(this.nationality.path) : "#";
  }
}
</script>

<style lang="scss">
.website-text {
  font-size: 14px;
  font-weight: bold;
}
.website-link {
  a {
    text-decoration: none;
    color: black;
  }
  a:hover {
    color: orange;
  }
}

.btn-light {
  a {
    text-decoration: none;
    color: black;
  }
  a:hover {
    color: orange;
  }
}

.image-nationality {
  max-width: 35px;
  max-height: 25px;
  object-fit: contain;
}
</style>
