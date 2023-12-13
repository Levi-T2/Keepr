<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" class="bg-dark border-0 selectable no-select rounded-circle" aria-labelledby="Login Dropdown"
          data-bs-toggle="dropdown" title="See Account Options">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="42" width="42"
              class="rounded-circle img-border" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div v-if="account.id" class="list-group">
            <RouterLink :to="{ name: 'Account', params: { accountId: account.id || user.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </RouterLink>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
import { RouterLink } from 'vue-router'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      }
    };
  },
  components: { RouterLink }
}
</script>

<style lang="scss" scoped>
.img-border {
  border: 1.5px solid rgba(12, 12, 12, 0.267);
  box-shadow: 1px 2px 5.25px 1.45px rgb(85, 85, 85);
  transition: ease-in-out 0.275s;
}

.img-border:hover {
  box-shadow: 1px 2px 5.25px 1.45px var(--bs-success);
}
</style>
