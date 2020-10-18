<template>
  <transition name="dialog" v-if="active">
    <div class="dialog-backdrop">
      <div class="dialog-container">
        <div class="recruitment-post">
          <h1 class="title-recuitment">
            Bạn đang ứng tuyển vị trí
            <span>{{ title }}</span>
          </h1>
          <div class="input-form row">
            <div class="col-2 p--relative">
              <label>Họ tên</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages" />
              <input type="text" class="form-control" v-model="cv.name" />
            </div>
          </div>
          <div class="input-form row">
            <div class="col-2 p--relative">
              <label>Số điện thoại</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages" />
              <input
                type="text"
                class="form-control"
                v-model="cv.phoneNumber"
              />
            </div>
          </div>
          <div class="input-form row">
            <div class="col-2 p--relative">
              <label>Email</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages" />
              <input type="email" class="form-control" v-model="cv.email" />
            </div>
          </div>
          <div v-if="hasCV" class="input-form row">
            <div class="col-2 p--relative">
              <label>Tải lên</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages">
                <span class="form-control-feedback">
                  Tải lên CV của bạn ( Hỗ trợ *.doc, *.docx, *.pdf)
                </span>
              </small>
              <!-- <input type="file" accept=".doc,.docx,.pdf" v-model="file" /> -->
              <b-form-file
                v-model="file"
                size="sm"
                plain
                accept=".doc,.docx,.pdf"
              />
            </div>
          </div>
          <div class="text-area-portfolio" v-if="hasCV">
            <p class="mt-2">Thư xin việc hoặc link portfolio:</p>
            <textarea
              type="text"
              rows="5"
              class="form-control mt-2"
              placeholder="Văn bản hoặc liên kết"
              v-model="cv.portfolio"
            />
          </div>
        </div>
        <div class="recuitment-footer mt-3">
          <button @click="closeDialog" class="btn btn-light">
            Đóng
          </button>
          <button
            @click="createRecruitment"
            type="button"
            class="btn-recuitment btn btn-warning"
          >
            {{ hasCV ? "Gửi CV" : "Ứng tuyển" }}
          </button>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { Guid } from "guid-typescript";
import { Vue, Component, Prop } from "vue-property-decorator";
import { Getter, Action } from "vuex-class";
import Util from "../../constants/util";
import { User } from "../../store/interfaces/authenticate";
import CV from "../../store/interfaces/cv";

@Component({
  name: "RecruitmentDialog",
  components: {},
})
export default class RecruitmentDialog extends Vue {
  @Prop({ type: Boolean, default: false }) private readonly hasCV!: boolean;
  @Prop({ type: String, default: "" }) private readonly title!: string;
  @Prop() private readonly postId!: Guid;
  @Getter("currentUser", { namespace: "AccountModule" })
  private currentUser!: User;
  @Action("createCV", { namespace: "RecruitmentModule" })
  private createCV!: (params: FormData) => Promise<void>;

  private message = "";

  private active = false;
  private defaultCV = {
    id: Guid.createEmpty(),
    userId: null,
    postId: Guid.createEmpty(),
    link: "",
    userName: "",
    email: "",
    portfolio: "",
    phoneNumber: "",
    name: "",
  } as CV;

  private cv = {
    id: Guid.createEmpty(),
    userId: null,
    postId: Guid.createEmpty(),
    link: "",
    userName: "",
    email: "",
    portfolio: "",
    phoneNumber: "",
    name: "",
  } as CV;

  private closeDialog() {
    this.active = false;
    this.cv = { ...this.defaultCV };
  }

  private file?: File | null = null;

  public openDialog() {
    this.cv.postId = this.postId;
    if (this.currentUser != null) {
      this.cv.userId = this.currentUser.id;
      this.cv.email = this.currentUser.emailAddress;
      this.cv.phoneNumber = this.currentUser.phoneNumber;
      this.cv.name = this.currentUser.name;
      this.cv.userName = this.currentUser.userName;
    }
    this.active = true;
    this.message = "";
  }

  private async createRecruitment() {
    const request = new FormData();
    if (this.cv.userId) {
      request.append("userId", `${this.cv.userId}`);
    }
    request.append("email", this.cv.email);
    request.append("phoneNumber", this.cv.phoneNumber);
    request.append("name", this.cv.name);
    request.append("userName", this.cv.userName);
    request.append("portfolio", this.cv.portfolio);
    request.append("postId", `${this.postId}`);
    if (this.file) {
      request.append("fileCV", this.file);
    }
    await this.createCV(request);
    this.$emit("create-success");
    this.active = false;
  }
}
</script>

<style lang="scss" scoped>
.dialog-backdrop {
  z-index: 1000;
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.5);
}

.dialog-container {
  max-width: 768px;
  box-shadow: 0 11px 15px -7px rgba(0, 0, 0, 0.2),
    0 24px 38px 3px rgba(0, 0, 0, 0.14), 0 9px 46px 8px rgba(0, 0, 0, 0.12);
  margin-top: 30px;
  margin-left: auto;
  margin-right: auto;
  padding: 15px;
  background-color: #fff;
}

.dialog-enter-active,
.dialog-leave-active {
  transition: opacity 0.2s;
}

.dialog-enter,
.dialog-leave-to {
  opacity: 0;
}

.dialog-enter-active .dialog-container,
.dialog-leave-active .dialog-container {
  transition: transform 0.4s;
}

.dialog-enter .dialog-container,
.dialog-leave-to .dialog-container {
  transform: scale(0.9);
}

.recruitment-post {
  .title-recuitment {
    font-size: 20px;
    span {
      color: red;
    }
  }

  .input-form {
    margin-top: 20px;
    .p--relative {
      position: relative;
      label {
        position: absolute;
        bottom: 0;
        margin-bottom: 0;
        font-weight: 700;
      }
    }

    .div-input {
      .form-text {
        display: block;
        margin-top: 0.25rem;
      }
    }

    .text-area-portfolio {
      border: 1px solid #ccc;
    }
  }
}

.recuitment-footer {
  text-align: right;
  .btn-recuitment {
    color: #fff;
    background: #d34127 !important;
  }
}
</style>
