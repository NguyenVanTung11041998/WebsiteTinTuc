<template>
  <div>
    <div class="container">
      <the-header />
      <div class="columns">
        <div class="column is-3">
            <menu-category-hashtag />
        </div>
        <div class="column is-9">
          <section class="section">
            <transition name="component-fade" mode="out-in">
              <router-view></router-view>
            </transition>
          </section>
        </div>
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
import MenuCategoryHashtag from "@/tin-tuc-app/views/MenuCategoryHashtag.vue";
import TheHeader from "@/tin-tuc-app/views/TheHeader.vue";

@Component({
  name: "App",
  components: { MenuCategoryHashtag, TheHeader }
})
export default class App extends Vue {}
</script>

<style scoped lang="scss">
</style>
