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
    </div>
    <div class="container-fluid">
        <section v-if="keeps.length" class="row" data-masonry>
            <div v-for="keep in keeps" class="col-6 col-md-4 col-lg-3">
                <KeepCard :keep="keep" />
            </div>
        </section>
        <section v-else class="row justify-content-center">
            <div class="col-12">
                <p>No Keeps In Vault </p>
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import KeepCard from '../components/KeepCard.vue';
import VaultHeader from '../components/VaultHeader.vue';
import masonry from 'masonry-layout';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        onMounted(() => {
            GetKeepsInVault();
            GetVaultById();
        });
        async function GetKeepsInVault() {
            try {
                AppState.keepsInVault = [];
                const vaultId = route.params.vaultId;
                await vaultsService.GetKeepsInVault(vaultId);
                await SetMasonry();
            }
            catch (error) {
                if (error.response.data.includes('!')) {
                    router.push({ name: 'Home' })
                } else {
                    Pop.error(error)
                }
            }
        }
        async function GetVaultById() {
            try {
                AppState.activeVault = null
                const vaultId = route.params.vaultId
                await vaultsService.GetVaultById(vaultId)
            } catch (error) {
                if (error.response.data.includes('!')) {
                    router.push({ name: 'Home' })
                } else {
                    Pop.error(error)
                }
            }
        }
        async function SetMasonry() {
            let row = document.querySelector("[data-masonry]")
            new masonry(row, { percentPosition: true })
        }
        return {
            route,
            router,
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