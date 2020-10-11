<template>
  <div class="login-container mt-4">
    <h1 class="title-login">Đăng nhập</h1>
    <div class="form-login mt-3">
      <div class="form-group">
        <div class="form-label-group row">
          <label class="col-2">Tài khoản</label>
          <input
            type="text"
            class="col-10 form-control"
            placeholder="Nhập tên tài khoản"
            v-model="userInfoRequest.userNameOrEmailAddress"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-2">Mật khẩu</label>
          <input
            type="password"
            class="col-10 form-control"
            placeholder="Nhập mật khẩu"
            v-model="userInfoRequest.password"
          />
        </div>
        <p v-if="isLoginFail" class="message-login mt-3">
          Sai tài khoản hoặc mật khẩu
        </p>
      </div>
      <div class="form-label-group row mt-3">
        <div class="col-1" />
        <div class="col-4">
          <button :disabled="buttonLoginStatus()" @click="loginApp" class="button-login btn btn-primary">
            Đăng nhập
          </button>
        </div>
        <div class="col-4 ml-5">
          <button class="button-register btn btn-success">
            <router-link :to="{ name: registerRouteName }">
              Đăng ký
            </router-link>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import RouteName from "../constants/route-name";
import { AuthenticateRequest } from "../store/interfaces/authenticate";

@Component({
  name: "Login",
  components: {},
})
export default class Login extends Vue {
  @Action("login", { namespace: "AccountModule" })
  private login!: (params: AuthenticateRequest) => Promise<void>;
  @Getter("loginStatus", { namespace: "AccountModule" })
  private loginStatus!: boolean;

  private isLoginFail = false;

  private userInfoRequest = {
    userNameOrEmailAddress: "",
    password: "",
    rememberClient: true,
  } as AuthenticateRequest;

  private readonly registerRouteName = RouteName.Register;

  private buttonLoginStatus(): boolean {
      return !(this.userInfoRequest.userNameOrEmailAddress
            && this.userInfoRequest.password
            && this.userInfoRequest.userNameOrEmailAddress.length > 0
            && this.userInfoRequest.password.length > 5);
  }

  private async loginApp(): Promise<void> {
    try {
        await this.login(this.userInfoRequest);
        this.$router.push({ name: RouteName.Home });
    }
    catch {
        this.isLoginFail = true;
    }
  }
}
</script>

<style lang="scss" scoped>
.login-container {
  text-align: center;
  .title-login {
    font-size: 30px;
    font-weight: bold;
  }

  .form-login {
    margin: auto;
    width: 50%;
    .form-label-group {
      label {
        padding-top: 8px;
        font-size: 16px;
        font-weight: 700;
      }
    }

    .message-login {
      color: red;
      font-size: 14px;
    }

    .button-login {
      width: 100%;
    }

    .button-register {
      width: 100%;
      a {
        color: white;
      }
    }
  }
}
</style>
