<template>
  <div class="parent-company-post-item">
    <div
      class="company-post-item"
      :class="heightCss"
      @mousemove="showInfo"
      v-if="!active"
    >
      <div class="company-logo">
        <img
          :src="getLinkPath(companyPost.thumbnail)"
          :title="companyPost.fullNameCompany"
        />
      </div>
      <h3 class="mt-1 ml-3">
        <strong>{{ companyPost.title }}</strong>
      </h3>
      <div class="mt-2 ml-3">
        <div v-if="companyPost.moneyType == 0">
          <p>
            {{
              `${companyPost.minSalary.toLocaleString("it-IT", {
                style: "currency",
                currency: "VND",
              })} - ${companyPost.maxSalary.toLocaleString("it-IT", {
                style: "currency",
                currency: "VND",
              })}`
            }}
          </p>
        </div>
        <div v-else>
          <p>Mức lương có thể thương lượng</p>
        </div>
      </div>
      <div class="mt-2 ml-3">
        <div>
          <span>{{ companyPost.location }}</span>
          <span class="time-create-post ml-2"> {{ getTime() }} </span>
        </div>
      </div>
    </div>
    <div
      class="company-post-item-show"
      :class="heightCss"
      @mouseleave="hideInfo"
      v-else
    >
      <div class="company-logo">
        <img
          :src="getLinkPath(companyPost.thumbnail)"
          :title="companyPost.fullNameCompany"
        />
      </div>
      <h3 class="mt-1 ml-3">
        <strong>{{ companyPost.title }}</strong>
      </h3>
      <div class="mt-2 ml-3">
        <div v-if="companyPost.moneyType == 0">
          <p>
            {{
              `${companyPost.minSalary.toLocaleString("it-IT", {
                style: "currency",
                currency: "VND",
              })} - ${companyPost.maxSalary.toLocaleString("it-IT", {
                style: "currency",
                currency: "VND",
              })}`
            }}
          </p>
        </div>
        <div v-else>
          <p>Mức lương có thể thương lượng</p>
        </div>
      </div>
      <div class="mt-2 ml-3">
        <div>
          <span>{{ companyPost.location }}</span>
          <span class="time-create-post ml-2"> {{ getTime() }} </span>
        </div>
      </div>
      <p class="mt-3 ml-3" v-html="companyPost.treatment"></p>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "../Home/TitleHot.vue";
import VueHorizontalList from "vue-horizontal-list"
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import CompanyPostModel from "../../store/interfaces/home";
import CONSTANT_VARIABLE from "../../constants/constant-variable";

@Component({
  name: "CompanyPostItem",
  components: {
    TitleHot,
    VueHorizontalList,
  },
})
export default class CompanyPostItem extends Vue {
  @Prop({ type: Object, default: null }) private readonly companyPost!: CompanyPostModel;
  private postRouteName = RouteName.Post;
  private active = false;
  private heightCss = "height-hide-info";

  private getLinkPath(file: IObjectFile): string {
    return file ? Util.getLinkPath(file.path) : "#";
  }

  private getTime(): string {
    return this.companyPost.timeCreateNewJob > CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY
      ? `${Math.floor(this.companyPost.timeCreateNewJob / CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY)} ngày trước`
      : `${this.companyPost.timeCreateNewJob} giờ trước`;
  }

  private showInfo(): void {
    this.active = true;
    this.heightCss = "height-show-info";
  }

  private hideInfo(): void {
    this.active = false;
    this.heightCss = "height-hide-info";
  }
}
</script>

<style lang="scss" scoped>
.parent-company-post-item {
  .height-hide-info {
    height: 220px;
    z-index: 1;
  }

  .height-show-info {
    height: auto;
    z-index: 1000;
  }

  .company-post-item,
  .company-post-item-show {
    overflow: hidden;
    white-space: nowrap;
    transition: 0.15s all ease-out;
    background: #f5f5f5;
    color: black;
    border: 1px solid #fdc578;
    box-sizing: border-box;
    .company-logo {
      height: 100px;
      width: 100px;
      font-size: 22px;
      line-height: 56px;
      display: flex;
      background: #fff;
      align-items: center;
      justify-content: center;
      padding: 5px;
      margin: 20px auto 3px;
      img {
        width: 100%;
        margin: auto auto;
        object-fit: cover;
        position: relative;
        z-index: 10;
        cursor: pointer;
      }
    }

    h3 {
      color: black;
      font-size: 16px;
      strong {
        display: block;
        overflow: hidden;
        color: #444;
        font-weight: 700;
        text-overflow: ellipsis;
        white-space: nowrap;
        text-align: left;
        transition: 0.2s all;
        text-transform: capitalize;
        font-size: 18px;
      }
    }

    .time-create-post {
      color: #118de5;
      text-align: right;
      text-overflow: ellipsis;
      overflow: hidden;
      white-space: nowrap;
    }
  }
}
</style>
