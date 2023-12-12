<template>
    <div class="col-12 vault-style mt-2">
        <div v-if="vault.creatorId == account.id">
            <button data-bs-toggle="modal" data-bs-target="#editVault" title="Edit Vault"
                class="btn btn-primary btn-edit"><i class="mdi mdi-pencil"></i></button>
        </div>
        <div class="txt-bg">
            <p class="fw-bold mb-0">{{ vault.name }}
                <span v-if="vault.isPrivate == true">
                    <i class="mdi mdi-lock"></i>
                </span>
            </p>
            <p class="mb-0">by: {{ vault.creator.name }}</p>
        </div>
    </div>
    <div class="col-12 my-3 text-center">
        <p class="mb-0">{{ vault.description }}</p>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props) {
        return {
            vaultImg: computed(() => `url(${props.vault.img})`),
            account: computed(() => AppState.account)
        }
    }
};
</script>


<style lang="scss" scoped>
.btn-edit {
    border-top-right-radius: 0px;
    border-top-left-radius: 0px;
    border-bottom-left-radius: 8px;
    border-bottom-right-radius: 8px;
}

.vault-style {
    background-image: v-bind(vaultImg);
    height: 16rem;
    width: 70%;
    background-position: center;
    background-size: cover;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
}

.txt-bg {
    background-color: rgba(30, 30, 30, 0.18);
    backdrop-filter: blur(12px);
    color: white;
    padding: 0.25rem;
    border-radius: 12px;
    font-size: 1.25em;
    text-align: center;
}
</style>