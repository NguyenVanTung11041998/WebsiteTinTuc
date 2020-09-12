<template>
  <div>
    <div>
      <the-header />
      <div class="container-content mt-3">
        <router-view />
      </div>
    </div>
    <div class="container">
      <footer>
        <hr />
        <p>&copy; 2020 - Created by Nguyễn Văn Tùng</p>
      </footer>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import TheHeader from "@/tin-tuc-app/views/TheHeader.vue";

@Component({
  name: "App",
  components: { TheHeader }
})
export default class App extends Vue {}
</script>

<style scoped lang="scss">
  .container-content {
    width: 90%;
    margin: auto;
  }
</style>
