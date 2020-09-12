<template>
  <div>
    <title-hot :title="pageTitle" />
    <div class="row content-company-prominent">
      <div class="col-lg-4">
        <vue-horizontal-list :items="items" :options="options">
          <template v-slot:default="{ item }">
            <div class="item">
              <h5>{{ item.title }}</h5>
              <p>{{ item.content }}</p>
            </div>
          </template>
        </vue-horizontal-list>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import TitleHot from "./TitleHot.vue";
import VueHorizontalList from "vue-horizontal-list";

@Component({
  name: "ProminentCompany",
  components: {
    TitleHot,
    VueHorizontalList,
  },
})
export default class ProminentCompany extends Vue {
  pageTitle: string = "Công Ty Nổi Bật";
  items = [
    { title: "Item 0", content: "Content item with description" },
    { title: "Item1", content: "Test" },
  ];
  options = {
    responsive: [
      { end: 576, size: 1 },
      { start: 576, end: 768, size: 2 },
      { start: 768, end: 992, size: 3 },
      { size: 4 },
    ],
    list: {
      // 1200 because @media (min-width: 1200px) and therefore I want to switch to windowed mode
      windowed: 1200,

      // Because: #app {padding: 80px 24px;}
      padding: 24,
    },
  };
}
</script>

<style lang="scss" scoped></style>
