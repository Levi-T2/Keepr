<template>
  <div class="container-fluid">
    <section v-if="keeps.length" class="row">
      <div v-for="keep in keeps" class="col-12 col-md-4 col-lg-3">
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
import { accountService } from '../services/AccountService';

export default {
  setup() {
    onMounted(() => {
      GetKeeps();
      GetMyVaults();
    });
    async function GetKeeps() {
      try {
        await keepsService.GetKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function GetMyVaults() {
      try {
        await accountService.GetMyVaults();
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss"></style>
