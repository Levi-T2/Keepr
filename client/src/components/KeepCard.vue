<template>
    <section class="row">
        <div class="col-12 p-2">
            <div class="img-container keep-card">
                <img title="See Keep Details" role="button" @click="OpenKeepModal(keep.id)" :src="keep.img" alt="Keep Image"
                    class="img-fluid rounded">
                <div class="top-right">
                    <div v-if="account.id == keep.creatorId && route.name != 'VaultDetails'">
                        <button title="Delete Keep" @click="DeleteKeep(keep.id)" class="btn btn-delete"><i
                                class="mdi mdi-close-thick"></i></button>
                    </div>
                    <div v-if="route.name == 'VaultDetails'">
                        <button @click="DeleteVaultKeep(keep.vaultKeepId)" title="Remove Keep From Vault"
                            class="btn btn-delete"><i class="mdi mdi-close-thick"></i></button>
                    </div>
                </div>
                <div title="See Keep Details" role="button" @click="OpenKeepModal(keep.id)" class="bottom-left">
                    <p class="txt-bg">{{ keep.name }}</p>
                </div>
                <div v-if="keep.creator" role="button" class="bottom-right creator-card">
                    <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creator.id } }"
                        :title="keep.creator.name">
                        <img :src="keep.creator.picture" alt="Creator Avatar" class="creator-img">
                    </RouterLink>
                </div>
            </div>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { RouterLink, useRoute } from 'vue-router';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { KeepInVault } from '../models/KeepInVault';

export default {
    props: {
        keep: { type: KeepInVault, required: true }
    },
    setup() {
        const route = useRoute()
        return {
            route,
            account: computed(() => AppState.account),
            async OpenKeepModal(keepId) {
                try {
                    AppState.activeKeep = null;
                    await keepsService.GetKeepById(keepId)
                    Modal.getOrCreateInstance("#keepDetails").show();
                }
                catch (error) {
                    Pop.error(error);
                }
            },
            async DeleteKeep(keepId) {
                try {
                    const wantsToDelete = await Pop.confirm(`Are you sure you want to delete this keep?`);
                    if (!wantsToDelete) {
                        return;
                    }
                    else {
                        await keepsService.DeleteKeep(keepId);
                    }
                }
                catch (error) {
                    Pop.error(error);
                }
            },
            async DeleteVaultKeep(vaultKeepId) {
                try {
                    const wantsToRemove = await Pop.confirm(`Are you sure you want to remove this from your vault?`)
                    if (!wantsToRemove) {
                        return
                    } else {
                        await vaultKeepsService.DeleteVaultKeep(vaultKeepId)
                    }
                } catch (error) {
                    Pop.error(error)
                }
            }
        };
    },
    components: { RouterLink }
};
</script>


<style lang="scss" scoped>
.creator-card {
    background-color: rgba(12, 12, 12, 0.267);
    backdrop-filter: blur(12px);
    padding: 0.25rem;
    border-radius: 25px;
    transition: ease-in-out 0.225s;
}

.creator-card:hover {
    background-color: rgb(50, 50, 50);
}

.btn-delete {
    background-color: rgba(12, 12, 12, 0.267);
    backdrop-filter: blur(12px);
    color: red;
    transition: ease-in-out 0.225s;
    border-bottom-left-radius: 17.5px;
    border-bottom-right-radius: 0px;
    border-top-left-radius: 0px;
    padding: 0.35rem 0.35rem 0.5rem 0.5rem;
}

.btn-delete:hover {
    background-color: red;
    color: white;
}

.keep-card {
    box-shadow: 1px 2px 6px 4px rgb(133, 133, 133);
    border-radius: 6px;
    transition: ease-in-out 0.275s;
}

.keep-card:hover {
    transform: translateY(0.3em);
    box-shadow: 1px 2px 8px 6px rgb(70, 70, 70);
}

.creator-img {
    height: 2.5em;
    width: 2.5em;
    border-radius: 25px;
}

.txt-bg {
    background-color: rgba(30, 30, 30, 0.18);
    backdrop-filter: blur(12px);
    color: white;
    padding: 0.25rem;
    border-radius: 6px;
    font-size: 1.25em;
    font-weight: bold;
}

// SECTION Css for img container

.img-container {
    position: relative;
    text-align: center;
}

.bottom-left {
    position: absolute;
    bottom: -8px;
    left: 8px;
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;
}

.top-right {
    position: absolute;
    top: 0px;
    right: 0px;
}

// !SECTION

@media screen and (max-width: 768px) {
    .txt-bg {
        font-size: 0.85em;
    }

    .creator-img {
        height: 2em;
        width: 2em;
    }
}
</style>