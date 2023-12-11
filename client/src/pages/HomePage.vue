<template>
  <div class="container-fluid">
    <section v-if="keeps.length" class="row">
      <div v-for="keep in keeps" class="col-4">
        <KeepCard :keep="keep" />
      </div>
    </section>
    <section v-else class="row justify-content-center">
      <div class="col-12">
        <p>Loading ...</p>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { AppState } from '../AppState';
import KeepCard from '../components/KeepCard.vue';

export default {
  setup() {
    onMounted(() => {
      GetKeeps();
    });
    async function GetKeeps() {
      try {
        await keepsService.GetKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
