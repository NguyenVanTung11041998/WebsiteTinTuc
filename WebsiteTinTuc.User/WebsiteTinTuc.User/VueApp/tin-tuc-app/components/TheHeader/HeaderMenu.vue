<template>
  <div class="header__topbar">
    <div class="row">
      <div class="col-lg-1" />
      <div class="col-lg-7">
        <ul class="list-menu">
          <li class="main-link">
            <ul>
              <li>
                <router-link :to="{ name: homeRouteName }">
                  Trang chủ
                </router-link>
              </li>
              <li>
                <router-link :to="{ name: listPostRouteName }">
                  Danh sách việc làm
                </router-link>
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
        <router-link
          v-if="!loginStatus"
          class="login-btn"
          :to="{ name: loginRouteName }"
        >
          Đăng nhập
        </router-link>
        <router-link
          v-if="!loginStatus"
          class="login-btn ml-3"
          :to="{ name: registerRouteName }"
        >
          Đăng ký
        </router-link>
        <router-link
          v-if="loginStatus && currentUser"
          class="login-btn"
          :to="{ name: editAccountRouteName }"
        >
          {{ currentUser.name }}
        </router-link>
        <button v-if="loginStatus" @click="logout" class="logout-btn ml-3">
          Đăng xuất
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Getter, Action, Mutation } from "vuex-class";
import CONSTANT_VARIABLE from "../../constants/constant-variable";
import RouteName from "../../constants/route-name";
//import { UserDetails } from '../../types/userModel'
import UrlAdminConst from "../../constants/url-constant";
import Authenticate, { User } from "../../store/interfaces/authenticate";

@Component({
  name: "HeaderMenu",
  components: {},
})
export default class HeaderMenu extends Vue {
  @Getter("userLoginInfo", { namespace: "AccountModule" })
  private userLoginInfo!: Authenticate;
  @Getter("loginStatus", { namespace: "AccountModule" })
  private loginStatus!: boolean;
  @Getter("currentUser", { namespace: "AccountModule" })
  private currentUser!: User;
  @Mutation("SET_USER_LOGIN_INFO", { namespace: "AccountModule" })
  private setUserLoginInfo!: (data: Authenticate | null) => void;
  @Mutation("SET_LOGIN_STATUS", { namespace: "AccountModule" })
  private setUserLoginStatus!: (status: boolean) => void;
  @Mutation("SET_CURRENT_USER", { namespace: "AccountModule" })
  private setCurrentUser!: (user: User | null) => void;

  private listPostRouteName = RouteName.ListPost;
  private homeRouteName = RouteName.Home;
  private postAdminUrl = UrlAdminConst.Post;
  private logout() {
    localStorage.setItem(CONSTANT_VARIABLE.APP_TOKEN, "");
    localStorage.setItem(CONSTANT_VARIABLE.APP_USERID, "");
    this.setUserLoginInfo(null);
    this.setUserLoginStatus(false);
    this.setCurrentUser(null);
  }

  private readonly loginRouteName = RouteName.Login;
  private readonly registerRouteName = RouteName.Register;
  private readonly editAccountRouteName = RouteName.EditAccount;
}
</script>

<style lang="scss" scoped>
.header__topbar {
  width: 100%;
  background-color: #000000;
  .list-menu {
    display: flex;
    margin-left: auto;
    .main-link {
      width: 100%;
      margin-right: 19px;
      ul {
        display: flex;
        li {
          margin-right: 10px;
          width: fit-content;
          a {
            display: inline-block;
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
    }
  }
  .header-margin {
    text-align: right;
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
      text-decoration: none;
    }
    .login-btn:hover {
      color: #d34127;
    }

    .logout-btn {
      border: 1px solid #fff;
      padding: 6px 10px;
      border-radius: 2px;
      color: #fff;
      background: black;
    }

    .logout-btn:hover {
      color: #d34127;
    }
  }
}
</style>
