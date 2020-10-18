<template>
  <div class="login-container mt-4">
    <h1 class="title-login">Chỉnh sửa tài khoản</h1>
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
            readonly
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
            Cập nhật
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
import AccountDto from "../store/interfaces/account";
import { AuthenticateRequest, User } from "../store/interfaces/authenticate";

@Component({
  name: "EditAccount",
  components: {},
})
export default class EditAccount extends Vue {
  @Action("updateUser", { namespace: "AccountModule" })
  private updateUser!: (params: AccountDto) => Promise<void>;
  @Action("getUserById", { namespace: "AccountModule" })
  private getUserById!: (id: number) => Promise<void>;
  @Getter("user", { namespace: "AccountModule" })
  private user!: AccountDto;
  @Getter("currentUser", { namespace: "AccountModule" })
  private currentUser!: User;

  private errorMessage = "";

  private userInfoRequest = {
    fullName: "",
    id: 0,
    lastLoginTime: null,
    creationTime: new Date(),
    userName: "",
    name: "",
    emailAddress: "",
    isActive: false,
    phoneNumber: "",
    roleNames: [],
  } as AccountDto;

  private confirmPassword = "";

  private readonly loginRouteName = RouteName.Login;

  private buttonRegisterStatus(): boolean {
    return !(
      this.userInfoRequest.userName &&
      this.userInfoRequest.name &&
      this.userInfoRequest.emailAddress &&
      this.userInfoRequest.phoneNumber
    );
  }

  private async register(): Promise<void> {
    if (this.buttonRegisterStatus()) {
      return;
    }
    try {
      await this.updateUser(this.userInfoRequest);
      this.$notify({
        type: "success",
        title: "",
        text: "Cập nhật tài khoản thành công!",
        duration: 100,
        speed: 2000,
      });
      this.$router.push({ name: RouteName.Login });
    } catch (e) {
      this.errorMessage = e.response.data.error.message;
    }
  }

  private async created() {
    await this.getUserById(this.currentUser.id);
    this.userInfoRequest = { ...this.user };
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
