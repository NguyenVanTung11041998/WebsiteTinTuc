<template>
  <div>
    <h1>Thay đỏi chuỗi kết nối database</h1>
    <br />
    <Form
      ref="postForm"
      label-position="top"
      :rules="configRule"
      :model="config"
    >
      <FormItem :label="L('Server name:')" prop="serverName">
        <Input v-model="config.serverName" />
      </FormItem>
      <FormItem :label="L('Database name:')" prop="databaseName">
        <Input v-model="config.databaseName" />
      </FormItem>
      <FormItem
        v-if="config.isAuthenticate"
        :label="L('User name:')"
        prop="userName"
      >
        <Input v-model="config.userName" />
      </FormItem>
      <FormItem
        v-if="config.isAuthenticate"
        :label="L('Password:')"
        prop="password"
      >
        <Input v-model="config.password" type="password" />
      </FormItem>
      <Checkbox v-model="config.isAuthenticate"
        >Đăng nhập bằng tài khoản</Checkbox
      >
      <div class="row footer" style="margin-top: 10px;">
        <Button @click="save" type="primary">{{ L("OK") }}</Button>
        <Button @click="reset" style="margin-left: 15px;" type="success">{{
          L("Reset")
        }}</Button>
      </div>
    </Form>
  </div>
</template>
<script lang="ts">
import { Component } from "vue-property-decorator";
import AbpBase from "../../../lib/abpbase";
import ConfigModel from "../../../store/entities/config";

@Component({})
export default class Config extends AbpBase {
  private config = {
    serverName: "",
    databaseName: "",
    userName: "",
    password: "",
    isAuthenticate: false,
  } as ConfigModel;

  private async created() {
    this.config = await this.$store.dispatch({
      type: "config/getConnectionString",
    });
  }

  private reset() {
    this.$Modal.confirm({
      title: this.L("Thông báo"),
      content: this.L("Reset cấu hình"),
      okText: this.L("Yes"),
      cancelText: this.L("No"),
      onOk: async () => {
        this.config = await this.$store.dispatch({
          type: "config/resetConnectionStringDefault",
        });
        this.$Message.success("Reset thành công");
      },
    });
  }

  private configRule = {
    serverName: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("name")),
        trigger: "blur",
      },
    ],
    databaseName: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("name")),
        trigger: "blur",
      },
    ],
  };

  private save() {
    this.$Modal.confirm({
      title: this.L("Thông báo"),
      content: this.L("Thay đổi cấu hình"),
      okText: this.L("Yes"),
      cancelText: this.L("No"),
      onOk: async () => {
        try {
          await this.$store.dispatch({
            type: "config/changeConnectionString",
            data: this.config,
          });
          this.$Message.success("Thay đổi thành công");
        } catch (error) {
          this.$Message.error(error.response.data.error.message);
        }
      },
    });
  }
}
</script>
<style lang="scss"></style>
