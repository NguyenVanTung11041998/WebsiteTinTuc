<template>
  <div v-if="!isLoading">
    <div>
      <the-header />
      <div class="container-content mt-3">
        <router-view />
      </div>
    </div>
    <div class="container">
      <footer>
        <hr />
        <p>&copy; 2020 - Trang tuyển dụng IT hàng đầu Việt Nam</p>
      </footer>
    </div>
    <notifications position="top center" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import TheHeader from "@/tin-tuc-app/views/TheHeader.vue";
import { Action } from "vuex-class";

@Component({
  name: "App",
  components: { TheHeader },
})
export default class App extends Vue {
  @Action("getCurrentLoginInformations", { namespace: "AccountModule" })
  private getCurrentLoginInformations!: () => Promise<void>;

  private isLoading = false;

  private async created(): Promise<void> {
    this.isLoading = true;
    await this.getCurrentLoginInformations();
    this.isLoading = false;
  }
}
</script>

<style scoped lang="scss">
.container-content {
  width: 90%;
  margin: auto;
}
.container {
  text-align: center;
}
</style>
