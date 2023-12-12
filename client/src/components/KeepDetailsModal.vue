<template>
    <div class="modal fade" id="keepDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-body">
                    <div v-if="activeKeep" class="container-fluid">
                        <section class="row">
                            <div class="col-12 d-flex justify-content-end">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="col-5">
                                <img :src="activeKeep.img" alt="Keep Image" class="keep-img">
                            </div>
                            <div class="col-7">
                                <div class="d-flex">
                                    <p class="px-1" title="Views"><i class="mdi mdi-eye-outline"></i> {{ activeKeep.views }}
                                    </p>
                                    <p class="px-1" title="Kept"><i class="mdi mdi-pin"></i>{{ activeKeep.kept }}</p>
                                </div>
                                <div>
                                    <p class="fs-4">{{ activeKeep.name }}</p>
                                    <p>{{ activeKeep.description }}</p>
                                </div>
                                <div v-if="Object.keys(account).length">
                                    <form @submit.prevent="SaveToVault(activeKeep.id)">
                                        <div class="d-flex flex-row">
                                            <select v-model="editable.vaultId" required class="form-select">
                                                <option :value="vault.id" v-for="vault in vaults" :key="vault.id"
                                                    id="vaultNames">
                                                    {{ vault.name }}
                                                </option>
                                            </select>
                                            <div class="px-2">
                                                <button type="submit" class="btn btn-primary">Save</button>
                                            </div>
                                        </div>
                                        <div id="inputHelp" class="form-text">Save to Vault</div>
                                    </form>
                                </div>
                                <div v-else>
                                    <p>You Have To Logged In To Save!</p>
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
                } catch (error) {
                    if (error.response.data.includes('Duplicate')) {
                        Pop.toast(`You've already saved that keep`)
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
.keep-img {
    height: auto;
    width: 16rem;
    background-position: center;
    background-size: cover;
}
</style>