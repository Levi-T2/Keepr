<template>
  <header class="sticky-top">
    <Neonav />
  </header>
  <main>
    <router-view />
  </main>

  <!-- Modals -->
  <KeepDetailsModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Neonav from './components/Neonav.vue'
import KeepDetailsModal from './components/KeepDetailsModal.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Neonav, KeepDetailsModal, CreateKeepModal, CreateVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
