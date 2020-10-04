<template>
  <div class="header__topbar">
    <div class="row">
      <div class="col-lg-1" />
      <div class="col-lg-8">
        <ul class="list-menu">
          <li class="main-link">
            <ul>
              <li>
                <router-link :to="{ name: listPostRouteName }"
                  >Danh sách việc làm</router-link
                >
              </li>
            </ul>
          </li>
        </ul>
      </div>
      <div class="header-margin">
        <a target="_blank" :href="postAdminUrl" class="btn bg--primary mr-2">
          <span>
            Đăng tuyển
            <i class="fa fa-arrow-right" />
          </span>
        </a>
        <a class="login-btn">Đăng nhập</a>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Getter, Action } from "vuex-class";
import RouteName from "../../constants/route-name";
//import { UserDetails } from '../../types/userModel'
import UrlAdminConst from "../../constants/url-constant";

@Component({
  name: "HeaderMenu",
  components: {},
})
export default class HeaderMenu extends Vue {
  // @Getter('userDetails', { namespace: 'AppModule' })
  // public userDetails!: UserDetails
  // @Getter('isAuthenticated', { namespace: 'AppModule' })
  // public isAuthenticated!: boolean

  // @Action('logout', { namespace: 'AppModule' })
  // public actionLogout!: () => void
  private listPostRouteName = RouteName.ListPost;
  private postAdminUrl = UrlAdminConst.Post;
  private isAuthenticated = false;
  private logout() {
    //this.actionLogout();
    this.$router.push("/home/sign-in");
  }

  private redirect() {
    window.open(
      "https://www.facebook.com/C%E1%BB%95ng-Game-H5-Metap-105893557565069/",
      "_blank"
    );
  }
}
</script>

<style lang="scss" scoped>
.header__topbar {
  width: 100%;
  background-color: #000000;
  .list-menu {
    float: left;
    margin-left: auto;
    .main-link {
      float: left;
      position: relative;
      margin-right: 19px;
      a {
        display: inline-block;
        position: relative;
        margin-top: 20px;
        font-size: 15px;
        line-height: 23px;
        color: #fff;
        text-decoration: none;
      }
      a:hover {
        color: #d34127;
      }
    }
  }
  .header-margin {
    margin-left: 20px;
    margin-top: 15px;
    margin-bottom: 15px;
    .bg--primary {
      color: #fff;
      background: #d34127 !important;
    }
    .bg--primary:hover {
      color: #ecd5d5;
      background: #5a2116;
    }
    .login-btn {
      border: 1px solid #fff;
      padding: 8px 10px;
      border-radius: 2px;
      color: #fff;
    }
    .login-btn:hover {
      color: #d34127;
    }
  }
}
</style>
