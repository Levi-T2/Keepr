<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-12 account-style">
                <div v-if="account.coverImg" class="img-container">
                    <img :src="account.coverImg" alt="Cover Image" class="cover-img">
                    <div class="img-position">
                        <img :src="account.picture" alt="Account Picture" class="account-img">
                    </div>
                </div>
                <div v-else class="img-container">
                    <img src="https://iwritingsolutions.com/wp-content/uploads/2022/05/d382f0a59d2d2ec86c613a04f9222ecd.jpg"
                        alt="PlaceHolder Image" class="cover-img">
                    <div class="img-position">
                        <img :src="account.picture" alt="Account Picture" class="account-img">
                    </div>
                </div>
                <div class="col-12 mt-5 p-0">
                    <div class="text-center">
                        <p class="txt-bg mb-0">{{ account.name }}</p>
                        <p class="mb-4">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
                        <p v-if="account.email">Email: {{ account.email }}</p>
                        <div v-if="account.bio">
                            <p>{{ account.bio }}</p>
                        </div>
                        <div v-if="route.name == 'Account'">
                            <button title="Edit Account" data-bs-toggle="modal" data-bs-target="#editAccount"
                                class="btn btn-primary rounded-pill">Edit <i class="mdi mdi-pencil"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <section v-if="vaults.length" class="row">
                <div class="text-center">
                    <p class="mb-0 fs-2">Vaults</p>
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
            <div class="text-center my-2">
                <p class="mb-0 fs-2">Keeps</p>
            </div>
        </section>
    </div>
    <div class="container-fluid">
        <section v-if="keeps.length" class="row" data-masonry>
            <div v-for="keep in keeps" class="col-6 col-md-4 col-lg-3">
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
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';
import { Account } from '../models/Account';
import AccountVaults from '../components/AccountVaults.vue'
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import { userService } from '../services/UserService'
import KeepCard from './KeepCard.vue';
import masonry from 'masonry-layout';

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
                    await SetMasonry()

                } else {
                    const profileId = route.params.profileId
                    const endpointUrl = `api/profiles/${profileId}/keeps`
                    await userService.GetKeepsForUser(endpointUrl)
                    await SetMasonry()
                }
            } catch (error) {
                Pop.error(error)
            }
        }
        async function SetMasonry() {
            let row = document.querySelector("[data-masonry]")
            new masonry(row, { percentPosition: true })
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
.txt-bg {
    color: black;
    padding: 0.25rem;
    font-size: 1.15em;
    font-weight: bold;
    text-align: center;
}

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

.edit-position {
    position: absolute;
    bottom: 4.5px;
    right: 12px;
}

// !SECTION
</style>