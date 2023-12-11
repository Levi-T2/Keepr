<template>
  <AccountDetails :account="account" />
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import AccountDetails from '../components/AccountDetails.vue';

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: { AccountDetails }
}
</script>

<style scoped lang="scss"></style>
