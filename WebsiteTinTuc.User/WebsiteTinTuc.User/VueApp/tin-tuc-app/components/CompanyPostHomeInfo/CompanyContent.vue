<template>
  <div class="col-lg-12">
    <div class="row">
      <div class="col-lg-8">
        <p v-html="description" />
      </div>
      <div class="col-lg-4">
        <div class="image-company">
          <img :src="getLinkPath(image)" />
        </div>
        <div class="mt-3 icon-button-image">
          <div
            v-for="(item, index) in images"
            :key="item.id"
            @click="changeImage(index)"
          >
            <i class="fas fa-circle ml-1" />
          </div>
        </div>
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
  name: "CompanyContent",
  components: {},
})
export default class CompanyContent extends Vue {
  @Prop() private readonly images!: IObjectFile[];
  @Prop({ type: String, default: "" }) private readonly description!: string;

  private image: IObjectFile = null;

  private getLinkPath(file: IObjectFile) {
    return file ? Util.getLinkPath(file.path) : "#";
  }

  created() {
    if (this.images && this.images.length > 0) {
      this.image = this.images[0];
    }
  }

  changeImage(index: number) {
    this.image = this.images[index];
  }
}
</script>

<style lang="scss" scoped>
.image-company {
  width: 250px;
  height: 150px;
  img {
    width: 100%;
    height: 100%;
    object-fit: contain;
    cursor: pointer;
  }
}

.icon-button-image {
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  svg {
    color: rgb(248, 144, 8);
  }
}
</style>
