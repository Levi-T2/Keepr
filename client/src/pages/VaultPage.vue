<template>
    <div class="container-fluid">
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

export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            GetKeepsInVault();
        });
        async function GetKeepsInVault() {
            try {
                const vaultId = route.params.vaultId;
                AppState.keepsInVault = [];
                await vaultsService.GetKeepsInVault(vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            route,
            keeps: computed(() => AppState.keepsInVault),
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>