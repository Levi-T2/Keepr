<template>
    <div class="container-fluid">
        <section v-if="vault" class="row justify-content-center">
            <VaultHeader :vault="vault" />
            <div class="col-10 d-flex justify-content-center">
                <div class="text-center mt-2 keeps-counter">
                    <p class="p-2 mb-0">{{ keeps.length }} Keeps</p>
                </div>
            </div>
        </section>
        <section v-else class="row">
            <div class="col-12">
                <p>Loading ...</p>
            </div>
        </section>
        <section v-if="keeps.length" class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-4">
                <KeepCard :keep="keep" />
            </div>
        </section>
        <section v-else class="row">
            <div class="col-12">
                <p>Loading ...</p>
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import KeepCard from '../components/KeepCard.vue';
import VaultHeader from '../components/VaultHeader.vue';

export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            GetKeepsInVault();
            GetVaultById();
        });
        async function GetKeepsInVault() {
            try {
                AppState.keepsInVault = [];
                const vaultId = route.params.vaultId;
                await vaultsService.GetKeepsInVault(vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function GetVaultById() {
            try {
                AppState.activeVault = null
                const vaultId = route.params.vaultId
                await vaultsService.GetVaultById(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            route,
            keeps: computed(() => AppState.keepsInVault),
            vault: computed(() => AppState.activeVault),
        };
    },
    components: { KeepCard, VaultHeader }
};
</script>


<style lang="scss" scoped>
.keeps-counter {
    background-color: rgb(194, 226, 255);
    border-radius: 10px;
}
</style>