<template>
  <div class="col-lg-6 post-info">
    <div class="row">
      <div class="col-lg-4 mt-2 mb-2">
        <router-link
          :to="{ name: postRouteName, params: { id: post.postUrl } }"
        >
          <img
            :src="getLinkPath(post.thumbnail)"
            :alt="`${post.companyName}-${post.fullNameCompany}`"
          />
        </router-link>
      </div>
      <div class="col-lg-8">
        <div class="title-post mt-3">
          <router-link
            :to="{ name: postRouteName, params: { id: post.postUrl } }"
          >
            <h3>{{ post.title }}</h3>
          </router-link>
          <p class="mt-2">{{ post.companyName }}</p>
          <p class="mt-2">
            <i class="fa fa-map-marker" />
            <span>{{ post.location }}</span>
          </p>
          <p class="mt-2">
            <span>
              <i class="fa fa-cog" />
              {{ post.companyJobs.map((x) => x.name).join(", ") }}
            </span>
          </p>
          <div class="row margin_hashtag_search">
            <div class="menu-hashtag block-selection mt-1">
              <ul class="hashtag-hot">
                <li v-for="item in post.postHashtags" :key="item.id">
                  <router-link
                    :to="{
                      name: hashtagRouteName,
                      params: { id: item.hashtagUrl },
                    }"
                  >
                    {{ item.name }}
                  </router-link>
                </li>
              </ul>
            </div>
          </div>
          <div class="mt-2 mb-2 time-post">
            <span>{{ getTime() }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";
import RouteName from "../../constants/route-name";
import CompanyPostModel from "../../store/interfaces/home";
import CONSTANT_VARIABLE from "../../constants/constant-variable";

@Component({
  name: "PostItem",
  components: {},
})
export default class PostItem extends Vue {
  @Prop() private readonly post!: CompanyPostModel;
  private postRouteName = RouteName.Post;
  private hashtagRouteName = RouteName.Hashtag;

  private getLinkPath(file: IObjectFile): string {
    return file ? Util.getLinkPath(file.path) : "#";
  }

  private getTime(): string {
    return this.post.timeCreateNewJob > CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY
      ? `${Math.floor(
          this.post.timeCreateNewJob / CONSTANT_VARIABLE.TOTAL_HOUR_OF_DAY
        )} ngày trước`
      : `${this.post.timeCreateNewJob} giờ trước`;
  }
}
</script>

<style lang="scss" scoped>
.post-info {
  background-color: #f5f5f5;
  .title-post {
    overflow: hidden;
    word-wrap: none;
    a {
      color: black;
      text-decoration: none;
      h3 {
        font-size: 18px;
        font-weight: 400;
      }
    }
    a:hover {
      color: coral;
    }

    .time-post {
      text-align: right;
      color: blue;
    }

    .margin_hashtag_search {
      margin: 0;
      .menu-hashtag {
        .hashtag-hot {
          display: flex;
          flex-flow: wrap;
          li {
            a {
              border: 1px solid rgba(0, 0, 0, 0.1);
              padding: 3px 6px;
              border-radius: 1px;
              display: block;
              margin: 3px;
              text-decoration: none;
              &:hover {
                background-color: #fa7973;
                border-color: #fa7973;
                color: #ffffff;
              }
            }
          }
        }
      }
    }
  }
}

.post-info:hover {
  background-color: pink;
}
</style>
