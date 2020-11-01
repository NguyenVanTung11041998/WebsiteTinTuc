<template>
  <div>
    <h1>Thay đỏi chuỗi kết nối database</h1>
    <br />
    <Form ref="postForm" label-position="top" :rules="postRule" :model="post">
      <FormItem :label="L('Server name:')" prop="serverName">
          <Input />
      </FormItem>

      <div class="row footer">
        <Button @click="cancel">{{ L("Cancel") }}</Button>
        <Button @click="save" type="primary">{{ L("OK") }}</Button>
      </div>
    </Form>
  </div>
</template>
<script lang="ts">
import { Component } from "vue-property-decorator";
import AbpBase from "../../../lib/abpbase";

@Component({})
export default class Config extends AbpBase {
    
}
</script>
<style lang="scss"></style>
