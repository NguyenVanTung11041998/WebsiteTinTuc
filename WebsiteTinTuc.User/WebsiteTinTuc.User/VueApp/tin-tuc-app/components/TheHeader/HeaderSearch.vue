<template>
  <div>
    <div class="header_search_form">
      <div class="row margin_input_search">
        <div class="col-lg-8">
          <b-input-group>
            <b-form-input
              @keyup.enter="searchGame"
              v-model="searchText"
              class="search"
              placeholder="Nhập từ khóa tìm kiếm"
              aria-label="Search"
            />
          </b-input-group>
        </div>
        <div class="col-lg-3">
          <b-form-select v-model="place" :options="options" />
        </div>
        <b-input-group-append class="btn_search">
          <a href="#">
            <b-button><i class="fas fa-search"/></b-button>
          </a>
        </b-input-group-append>
      </div>
      <div style="margin: 1em;" v-if="!isLoading">
        <div class="row margin_hashtag_search">
          <div class="menu-hashtag block-selection mt-1">
            <ul class="hashtag-hot">
              <li v-for="item in hashtags" :key="item.id">
                <router-link
                  :to="{ name: 'post', params: { id: item.hashtagUrl } }"
                >
                  {{ item.name }}
                </router-link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import Hashtag from "../../store/interfaces/hashtag";
@Component({
  name: "HeaderSearch",
  components: {},
})
export default class HeaderSearch extends Vue {
  private searchText: string = "";
  private place: number = 0;
  private options = [
    {
      value: 0,
      text: "Tất cả địa điểm",
    },
    {
      value: 1,
      text: "Hà Nội",
    },
    {
      value: 2,
      text: "Đà Nẵng",
    },
    {
      value: 3,
      text: "Thành Phố Hồ Chí Minh",
    },
  ];

  private isLoading = false;

  @Action("getAllHashtagIsHot", { namespace: "HashtagModule" })
  public getAllHashtagIsHot!: () => void;
  @Getter("hashtags", { namespace: "HashtagModule" })
  public hashtags!: Hashtag[];

  private async created() {
    this.isLoading = true;
    await this.getAllHashtagIsHot();
    this.isLoading = false;
  }
}
</script>

<style lang="scss" scoped>
.header_search_form {
  width: 90%;
  margin: auto;
  border: 1px solid red;
  .margin_input_search {
    margin: 1em;
    button {
      background: white;
      color: #333;
    }
    button:hover {
      color: red;
    }
  }

  .margin_hashtag_search {
    margin: 1em;
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
</style>
