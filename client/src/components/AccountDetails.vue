<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-12 account-style">
                <div class="img-container">
                    <img :src="account.coverImg" alt="Cover Image" class="cover-img">
                    <div class="img-position">
                        <img :src="account.picture" alt="Account Picture" class="account-img">
                    </div>
                </div>
                <div class="col-12 mt-5 p-0">
                    <div class="text-center">
                        <p>{{ account.name }}</p>
                        <p v-if="account.email">Email: {{ account.email }}</p>
                        <div v-if="account.bio">
                            <p>{{ account.bio }}</p>
                        </div>
                        <div v-if="route.name == 'Account'">

                            <button title="Edit Account" data-bs-toggle="modal" data-bs-target="#editAccount"
                                class="btn btn-primary rounded-circle"><i class="mdi mdi-pencil"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <section v-if="vaults.length" class="row">
                <div class="text-center">
                    <p class="mb-0 fs-3">Vaults</p>
                </div>
                <div v-for="vault in vaults" :key="vault.id" class="col-12 col-md-4 col-lg-3 mt-2">
                    <AccountVaults :vault="vault" />
                </div>
            </section>
            <section v-else class="row">
                <div class="col-12">
                    <div class="text-center mt-1">
                        <p class="mb-0 fs-3">Loading ...</p>
                    </div>
                </div>
            </section>
            <section v-if="keeps.length" class="row">
                <div class="text-center">
                    <p class="mb-0 fs-3">Keeps</p>
                </div>
                <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-4 col-lg-3 mt-2">
                    <KeepCard :keep="keep" />
                </div>
            </section>
            <section v-else class="row">
                <div class="col-12">
                    <div class="text-center mt-1">
                        <p class="mb-0 fs-3">Loading ...</p>
                    </div>
                </div>
            </section>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';
import { Account } from '../models/Account';
import AccountVaults from '../components/AccountVaults.vue'
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { useRoute } from 'vue-router';
import { userService } from '../services/UserService'
import KeepCard from './KeepCard.vue';

export default {
    props: {
        account: { type: Account, required: true }
    },
    setup() {
        const route = useRoute()
        onMounted(() => {
            GetVaultsForUser();
            GetKeepsForUser();
        })
        async function GetVaultsForUser() {
            try {
                AppState.vaults = []
                const accountId = route.params.accountId
                if (route.path == `/account/${accountId}`) {
                    const endpointUrl = `account/vaults`
                    await userService.GetVaultsForUser(endpointUrl)
                } else {
                    const profileId = route.params.profileId
                    const endpointUrl = `api/profiles/${profileId}/vaults`
                    await userService.GetVaultsForUser(endpointUrl)
                }
            } catch (error) {
                Pop.error(error)
            }
        }
        async function GetKeepsForUser() {
            try {
                AppState.keepsForUser = []
                const accountId = route.params.accountId
                if (route.path == `/account/${accountId}`) {
                    const endpointUrl = `api/profiles/${accountId}/keeps`
                    await userService.GetKeepsForUser(endpointUrl)
                } else {
                    const profileId = route.params.profileId
                    const endpointUrl = `api/profiles/${profileId}/keeps`
                    await userService.GetKeepsForUser(endpointUrl)
                }
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            route,
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keepsForUser),
        }
    },
    components: { AccountVaults, KeepCard }
};
</script>


<style lang="scss" scoped>
.cover-img {
    height: 18rem;
    width: 100%;
    background-position: center;
    background-size: cover;
    border-radius: 10px;
}

.account-img {
    background-position: center;
    height: 6rem;
    width: 6rem;
    border-radius: 22px;
}

.account-style {
    padding: 1%;
}

// SECTION Css for cover img position

.img-container {
    position: relative;
    text-align: center;
}

.img-position {
    position: absolute;
    top: 100%;
    left: 50%;
    transform: translate(-50%, -50%);
}

// !SECTION
</style>