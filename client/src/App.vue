<template>
  <header class="sticky-top navbar-desktop">
    <Neonav />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="sticky-bottom navbar-mobile">
    <NavbarMobile />
  </footer>

  <!-- Modals -->
  <KeepDetailsModal />
  <CreateKeepModal />
  <CreateVaultModal />
  <EditAccountModal />
  <EditVaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Neonav from './components/Neonav.vue'
import KeepDetailsModal from './components/KeepDetailsModal.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
import EditAccountModal from './components/EditAccountModal.vue'
import NavbarMobile from './components/NavbarMobile.vue'
import EditVaultModal from './components/EditVaultModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Neonav, KeepDetailsModal, CreateKeepModal, CreateVaultModal, EditAccountModal, NavbarMobile, EditVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

.navbar-mobile {
  display: none;
}

@media screen and (max-width: 768px) {
  .navbar-desktop {
    display: none;
  }

  .navbar-mobile {
    display: block;
  }
}
</style>
