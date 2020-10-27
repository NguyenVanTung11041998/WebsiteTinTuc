<template>
  <div class="login-container mt-4">
    <h1 class="title-login">Đăng ký</h1>
    <div class="form-login mt-3">
      <div class="form-group">
        <div class="form-label-group row">
          <label class="col-4">Họ tên</label>
          <input
            type="text"
            class="col-8 form-control"
            placeholder="Nhập họ tên"
            v-model="userInfoRequest.name"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-4">Email</label>
          <input
            type="email"
            class="col-8 form-control"
            placeholder="Nhập email"
            v-model="userInfoRequest.emailAddress"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-4">Số điện thoại</label>
          <input
            type="text"
            class="col-8 form-control"
            placeholder="Nhập số điện thoại"
            v-model="userInfoRequest.phoneNumber"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-4">Tài khoản</label>
          <input
            type="text"
            class="col-8 form-control"
            placeholder="Nhập tên tài khoản"
            v-model="userInfoRequest.userName"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-4">Mật khẩu</label>
          <input
            type="password"
            class="col-8 form-control"
            placeholder="Nhập mật khẩu"
            v-model="userInfoRequest.password"
          />
        </div>
        <div class="form-label-group row mt-3">
          <label class="col-4">Nhập lại mật khẩu</label>
          <input
            type="password"
            class="col-8 form-control"
            placeholder="Nhập lại mật khẩu"
            v-model="confirmPassword"
          />
        </div>
        <p v-if="errorMessage" class="message-login mt-3">
          {{ errorMessage }}
        </p>
      </div>
      <div class="form-label-group row mt-3">
        <div class="col-1" />
        <div class="col-4">
          <button
            :disabled="buttonRegisterStatus()"
            @click="register"
            class="button-register btn btn-primary"
          >
            Đăng ký
          </button>
        </div>
        <div class="col-4 ml-5">
          <router-link class="button-login" :to="{ name: loginRouteName }">
            <button class="btn btn-success">
              Đăng nhập
            </button>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import RouteName from "../constants/route-name";
import { CreateAccount } from "../store/interfaces/account";
import { AuthenticateRequest } from "../store/interfaces/authenticate";

@Component({
  name: "Register",
  components: {},
})
export default class Register extends Vue {
  @Action("createUser", { namespace: "AccountModule" })
  private createUser!: (params: CreateAccount) => Promise<void>;

  private errorMessage = "";
  private regexPasswordPattern =
    "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{10,})";

  private userInfoRequest = {
    password: "",
    userName: "",
    name: "",
    emailAddress: "",
    isActive: true,
    phoneNumber: "",
    roleNames: ["Hr", "User"],
  } as CreateAccount;

  private confirmPassword = "";

  private readonly loginRouteName = RouteName.Login;

  private buttonRegisterStatus(): boolean {
    return !(
      this.userInfoRequest.userName &&
      this.userInfoRequest.password &&
      this.userInfoRequest.name &&
      this.userInfoRequest.emailAddress &&
      this.userInfoRequest.phoneNumber &&
      this.userInfoRequest.password.length > 9 &&
      this.checkConfirmPassword()
    );
  }

  private checkConfirmPassword() {
    if (this.userInfoRequest.password == this.confirmPassword) {
      this.errorMessage = "";
      return true;
    }
    this.errorMessage = "Mật khẩu không khớp";
    return false;
  }

  private async register(): Promise<void> {
    if (this.buttonRegisterStatus()) {
      return;
    }

    let regex = new RegExp(this.regexPasswordPattern);

    if (!regex.test(this.confirmPassword)) {
      this.$notify({
        type: "error",
        title: "",
        text: "Mật khẩu phải từ 10 ký tự, chứa chữ hoa, thường và ký tự đặc biệt",
        duration: 100,
        speed: 2000,
      });
      return;
    }

    try {
      await this.createUser(this.userInfoRequest);
      this.$notify({
        type: "success",
        title: "",
        text: "Đăng ký tài khoản thành công!",
        duration: 100,
        speed: 2000,
      });
      this.$router.push({ name: RouteName.Login });
    } catch (e) {
      this.errorMessage = e.response.data.error.message;
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
        text-align: left;
      }
    }

    .message-login {
      color: red;
      font-size: 14px;
    }

    .button-register {
      width: 100%;
    }

    .button-login {
      color: white;
      text-decoration: none;
      button {
        width: 100%;
      }
    }
  }
}
</style>
