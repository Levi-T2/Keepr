<template>
    <div class="modal fade" id="keepDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div class="modal-body">
                    <div v-if="activeKeep" class="container-fluid">
                        <section class="row justify-content-between">
                            <div class="col-12 col-md-6 order-1 order-md-2 p-0 d-flex justify-content-end">
                                <div class="img-container">
                                    <img :src="activeKeep.img" alt="Keep Image" class="keep-img">
                                    <div class="top-right">
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"
                                            aria-label="Close"></button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-md-6 order-2 order-md-1 p-4 d-flex flex-column justify-content-between">
                                <div>
                                    <div class="d-flex justify-content-start">
                                        <p class="px-1" title="Views"><i class="mdi mdi-eye-outline"></i> {{
                                            activeKeep.views }}
                                        </p>
                                        <p class="px-1" title="Kept"><i class="mdi mdi-pin"></i>{{ activeKeep.kept }}</p>
                                    </div>
                                    <div class="text-start">
                                        <p class="fs-3 txt-cus">{{ activeKeep.name }}</p>
                                        <p>{{ activeKeep.description }}</p>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-around align-items-center w-100">
                                    <div v-if="Object.keys(account).length" class="form-width">
                                        <form @submit.prevent="SaveToVault(activeKeep.id)">
                                            <div class="d-flex">
                                                <select v-model="editable.vaultId" required class="form-select">
                                                    <option :value="vault.id" v-for="vault in vaults" :key="vault.id"
                                                        id="vaultNames">
                                                        {{ vault.name }}
                                                    </option>
                                                </select>
                                                <div class="px-2">
                                                    <button title="Save To Vault" type="submit" class="btn btn-primary"><i
                                                            class="mdi mdi-content-save"></i></button>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                    <div v-else>
                                        <p>You Have To Logged In To Save!</p>
                                    </div>
                                    <RouterLink :to="{ name: 'Profile', params: { profileId: activeKeep.creator.id } }"
                                        title="To User Profile">
                                        <div class="profile-card">
                                            <p class="mb-0 px-2 text-dark">{{ activeKeep.creator.name }}</p>
                                            <img alt="Profile Picture" :src="activeKeep.creator.picture"
                                                class="profile-img">
                                        </div>
                                    </RouterLink>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            activeKeep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.usersVaults),
            async SaveToVault(keepId) {
                try {
                    const vaultKeepData = editable.value
                    vaultKeepData.keepId = keepId
                    await vaultKeepsService.CreateVaultKeep(vaultKeepData)
                    editable.value = {}
                    Pop.toast(`Keep Saved To Vault`)
                } catch (error) {
                    if (error.response.data.includes('Duplicate')) {
                        Pop.toast(`You've already saved that keep to that vault.`)
                    } else {
                        Pop.error(error)
                    }
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.profile-card {
    display: flex;
    align-items: center;
    border: 2px solid rgba(220, 220, 220, 0);
    border-radius: 8px;
    padding: 0.15rem;
    transition: ease-in-out 0.275s;
}

.profile-card:hover {
    border: 2px solid gainsboro;
}

.form-width {
    width: 100%;
    display: flex;
    justify-content: space-between;
}

.modal-body {
    padding: 0%;
}

.txt-cus {
    font-family: 'Playfair Display', serif;
}


.profile-img {
    height: 3rem;
    width: 3rem;
    background-position: center;
    background-size: cover;
    border-radius: 25px;
}

.keep-img {
    min-height: 28rem;
    max-height: 38.155rem;
    min-width: 32.25rem;
    max-width: 37.5rem;
    background-position: center;
    background-size: cover;
    border-top-right-radius: 8px;
    border-bottom-right-radius: 8px;
}

.img-container {
    position: relative;
    min-height: 28rem;
    max-height: 38.155rem;
    min-width: 32.25rem;
    max-width: 37.5rem;
}

.top-right {
    position: absolute;
    top: 16px;
    right: 16px;
}

@media screen and (max-width: 768px) {
    .keep-img {
        max-width: 25.75em;
        border-top-left-radius: 0px;
        border-bottom-right-radius: 8px;
    }
}
</style>