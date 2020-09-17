<template>
  <div class="row">
    <div class="col-lg-7">
      <title-hot :title="'Công Ty Nổi Bật'" />
      <div class="row content-company-prominent">
        <div class="col-lg-12" v-if="!isLoading">
          <vue-horizontal-list :items="prominentCompanies" :options="options">
            <template v-slot:default="{ item }">
              <div class="row">
                <div class="col-lg-5">
                  <div class="item">
                    <router-link
                      :to="{ name: 'company', params: { id: item.companyUrl } }"
                    >
                      <img :src="getLinkPath(item.image)" :alt="item.name" />
                      <div class="image-item-thumbnail">
                        <img
                          :src="getLinkPath(item.thumbnail)"
                          :alt="item.name"
                          :title="item.name"
                        />
                      </div>
                    </router-link>
                  </div>
                </div>
                <div class="col-lg-5 name-spotlight">
                  <h3 class="title-spotlight mb-4">{{ item.name }}</h3>
                  <p>{{ item.fullNameCompany }}</p>
                  <p v-html="item.description" />
                  <p>{{ item.location }}</p>
                  <div class="mt-2 link">
                    <router-link
                      :to="{ name: 'company', params: { id: item.companyUrl } }"
                    >
                      {{
                        item.numberJobs > 0
                          ? `${item.numberJobs} jobs`
                          : "Xem thêm"
                      }}
                      <i class="ml-1 fa fa-arrow-right" />
                    </router-link>
                  </div>
                </div>
              </div>
            </template>
          </vue-horizontal-list>
        </div>
      </div>
    </div>
    <div class="col-lg-4">
      <div class="row">
        <title-hot style="padding-left: 15px;" :title="'Công việc hot'" />
        <div class="col-lg-12" style="margin-top: 24px;">
          <div
            class="boder-post-prominent"
            v-for="item in prominentPosts"
            :key="item.id"
          >
            <div class="row">
              <div class="col-lg-8">
                <div class="post-prominent">
                  <div class="ml-3 mt-3 company-name-post">
                    <p>{{ item.companyName }}</p>
                  </div>
                  <span class="ml-3 mt-2">
                    <router-link
                      :to="{ name: 'post', params: { id: item.postUrl } }"
                    >
                      {{ item.title }}
                    </router-link>
                  </span>
                  <p class="mt-2 ml-3">
                    <span>
                      <a href="#">Đăng nhập để ứng tuyển</a>
                    </span>
                  </p>
                </div>
              </div>
              <div class="col-lg-4 icon-info-prominent">
                <div class="icon-info-post-prominent ml-1">
                  <router-link
                    :to="{ name: 'post', params: { id: item.postUrl } }"
                  >
                    <i class="fas fa-arrow-alt-circle-right" />
                  </router-link>
                </div>
                <div class="link-post-prominent">
                  <router-link
                    :to="{ name: 'post', params: { id: item.postUrl } }"
                  >
                    Xem thêm
                  </router-link>
                </div>
              </div>
            </div>
          </div>
        </div>
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
import { ProminentCompanyModel } from "../../store/interfaces/company";
import { PostTopProminent } from "../../store/interfaces/post";

@Component({
  name: "ProminentCompany",
  components: {
    TitleHot,
    VueHorizontalList,
  },
})
export default class ProminentCompany extends Vue {
  @Action("getTopCompanyProminent", { namespace: "CompanyModule" })
  private getAllHashtagIsHot!: (count?: number) => void;
  @Getter("prominentCompanies", { namespace: "CompanyModule" })
  private prominentCompanies!: ProminentCompanyModel[];

  @Action("getTopCompanyProminent", { namespace: "PostModule" })
  private getTopCompanyProminent!: (count?: number) => void;
  @Getter("prominentPosts", { namespace: "PostModule" })
  private prominentPosts!: PostTopProminent[];

  private isLoading = true;

  private async created() {
    const count = 3;
    this.isLoading = true;
    await this.getAllHashtagIsHot(count);
    await this.getTopCompanyProminent(count);
    this.isLoading = false;
  }

  private getLinkPath(file: IObjectFile): string {
    return file ? Util.getLinkPath(file.path) : "#";
  }

  options = {
    responsive: [{ size: 1 }],
    list: {
      windowed: 1200,
      padding: 24,
    },
  };
}
</script>

<style lang="scss" scoped>
.content-company-prominent {
  height: 400px;
  max-height: 400px;
  div {
    height: 100%;
  }
  .item {
    img {
      object-fit: contain;
    }
    width: 100%;
    position: relative;
    z-index: 1;
    .image-item-thumbnail {
      z-index: 100;
      position: absolute;
      top: 10px;
      right: 10px;
      width: 40%;
      height: 70px;
      img {
        object-fit: contain;
      }
    }
  }

  .name-spotlight {
    .title-spotlight {
      display: block;
      font-size: 20px;
      font-weight: 700;
      color: #d34127;
    }
    p {
      font-size: 14px;
      margin-top: 10px;
      line-height: 140%;
    }

    .link a {
      text-decoration: none;
      color: #d34127;
    }
  }
  .image-thumbnail-company {
    margin-top: 24px;
    max-height: calc(100% - 24px);
    width: 90%;
    .image-thumbnail {
      max-height: 33.33%;
      border: 1px solid;
      cursor: pointer;
      img {
        width: 100%;
        object-fit: contain;
        height: 100%;
      }
    }
  }
}

.boder-post-prominent {
  border: 1px solid;
  max-height: 100%;
  .post-prominent {
    .company-name-post {
      display: flow-root;
      overflow: hidden;
      margin-bottom: 1px;
      width: 100%;
      height: 17px;
      font-size: 14px;
      line-height: 17px;
      p {
        display: inline-block;
        color: #999;
        font-size: 12px;
      }
    }
    span {
      display: block;
      color: #444;
      line-height: 17px;
      width: 80%;
      font-weight: 700;
      margin-bottom: 1px;
      a:hover {
        text-decoration: none;
        cursor: pointer;
        color: #d34127;
        transition: 0.3s;
      }
    }
    p {
      span {
        color: #d34127;
        text-decoration: underline;
        font-style: italic;
        font-size: 11px;
        a {
          color: #d34127 !important;
        }
        a:hover {
          text-decoration: none;
          color: #d34127;
          transition: 0.3s;
        }
      }
    }
  }
  .icon-info-prominent {
    position: relative;
    .icon-info-post-prominent {
      top: 30%;
      position: absolute;
      transform: scale(2);
    }
    .link-post-prominent {
      position: absolute;
      top: 65%;
      a {
        color: red;
      }
    }
  }
}
</style>
