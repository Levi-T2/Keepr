<template>
    <section class="row">
        <div v-if="account.id == vault.creatorId" class="delete-btn-position">
            <button @click="DeleteVault(vault.id)" class="vault-btn"><i class="mdi mdi-close-thick"></i></button>
        </div>
        <RouterLink class="p-1" :to="{ name: 'VaultDetails', params: { vaultId: vault.id } }" title="See Vault...">
            <div class="col-12 vault-card">
                <p class="m-2 txt-bg">
                    <span v-if="vault.isPrivate == true">
                        <i class="mdi mdi-lock"></i>
                    </span>
                    {{ vault.name }}
                </p>
            </div>
        </RouterLink>
    </section>
</template>


<script>
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { RouterLink } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { AppState } from '../AppState';

export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props) {
        return {
            account: computed(() => AppState.account),
            vaultImg: computed(() => `url(${props.vault.img})`),
            async DeleteVault(vaultId) {
                try {
                    const wantsToDelete = await Pop.confirm(`Are you sure you want to delete this vault?`)
                    if (!wantsToDelete) {
                        return
                    } else {
                        await vaultsService.DeleteVault(vaultId)
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
.delete-btn-position {
    display: flex;
    justify-content: end;
    margin-left: 0.8rem;
}

.vault-btn {
    position: absolute;
    z-index: 1;
    width: auto;
    border-radius: 35px;
    background-color: rgb(54, 54, 54);
    color: red;
    border: 1px solid red;
    transition: ease-in-out 0.275s;
}

.vault-btn:hover {
    transform: translateY(-0.25rem);
    background-color: rgb(154, 154, 154);
}

.vault-card {
    background-image: v-bind(vaultImg);
    height: 14rem;
    width: 100%;
    background-position: center;
    background-size: cover;
    border-radius: 8px;
    box-shadow: 2px 3px 7px 3px rgb(133, 133, 133);
    display: flex;
    align-items: flex-end;
    transition: ease-in-out 0.275s;
}

.vault-card:hover {
    transform: translateY(0.25em);
    box-shadow: 2px 3px 6px 4px rgb(67, 67, 67);
}

.txt-bg {
    background-color: rgba(8, 8, 8, 0.247);
    backdrop-filter: blur(10px);
    color: white;
    padding: 0.25rem;
    border-radius: 6px;
    font-size: 1.25em;
    font-weight: bold;
}

// @media screen and (max-width: 768px) {
//     .vault-img {
//         height: 14rem;
//         width: auto;
//         background-position: center;
//         background-size: cover;
//         border-radius: 8px;
//     }
// }
</style>