<template>
  <transition name="dialog">
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
              <input type="text" class="form-control" />
            </div>
          </div>
          <div class="input-form row">
            <div class="col-2 p--relative">
              <label>Số điện thoại</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages" />
              <input type="text" class="form-control" />
            </div>
          </div>
          <div class="input-form row">
            <div class="col-2 p--relative">
              <label>Email</label>
            </div>
            <div class="col-10 div-input">
              <small class="form-text text-muted messages" />
              <input type="email" class="form-control" />
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
              <input type="file" accept=".doc,.docx,.pdf" />
            </div>
          </div>
          <div class="text-area-portfolio" v-if="hasCV">
            <p class="mt-2">Thư xin việc hoặc link portfolio:</p>
            <textarea
              type="text"
              rows="5"
              class="form-control mt-2"
              placeholder="Văn bản hoặc liên kết"
            />
          </div>
        </div>
        <div class="recuitment-footer mt-3">
          <button @click="handleBackdropClick" class="btn btn-light">
            Đóng
          </button>
          <button type="submit" class="btn-recuitment btn btn-warning">
            {{ hasCV ? "Gửi CV" : "Ứng tuyển" }}
          </button>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import Util from "../../constants/util";
import IObjectFile from "../../store/interfaces/IObjectFile";

@Component({
  name: "RecruitmentDialog",
  components: {},
})
export default class RecruitmentDialog extends Vue {
  @Prop({ type: Boolean, default: false }) private readonly active!: boolean;
  @Prop({ type: Boolean, default: false }) private readonly hasCV!: boolean;
  @Prop({ type: String, default: "" }) private readonly title!: string;

  private handleBackdropClick() {
    this.$emit("close-dialog", false);
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
