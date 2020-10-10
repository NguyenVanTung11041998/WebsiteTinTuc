<template>
  <div class="col-lg-12">
    <div class="row title-thumbnail-company">
      <div class="col-lg-3 img-thumbnail mt-2 mb-2">
        <img :src="getLinkPath()" :alt="companyName" />
      </div>
      <div class="col-lg-8 mt-2 mb-2 ml-2">
        <h2 class="company-name">{{ companyName }}</h2>
        <p class="mt-2">{{ fullNameCompany }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import TitleHot from "../Home/TitleHot.vue";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";

@Component({
  name: "HeaderThumbnail",
  components: {},
})
export default class HeaderThumbnail extends Vue {
  @Prop({ type: Object, default: null })
  private readonly thumbnail!: IObjectFile;
  @Prop({ type: String, default: "" }) private readonly companyName!: string;
  @Prop({ type: String, default: "" })
  private readonly fullNameCompany!: string;

  private getLinkPath() {
    return this.thumbnail ? Util.getLinkPath(this.thumbnail.path) : "#";
  }
}
</script>

<style lang="scss" scoped>
.title-thumbnail-company {
    background-color: #f5f5f5;
  .img-thumbnail {
    margin-left: 15px;
    width: 170px;
    height: 90px;
    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
    }
  }

  .company-name {
    font-size: 16px;
    font-weight: bold;
  }
}
</style>
