<template>
    <div class="modal fade" id="editVault" tabindex="-1" aria-labelledby="editVaultModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Edit Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="EditVault()">
                        <div class="my-2">
                            <label for="editVaultName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="editVaultName"
                                maxlength="300">
                        </div>
                        <div class="my-2">
                            <label for="editVaultDescription" class="form-label">Description</label>
                            <textarea v-model="editable.description" type="text" class="form-control"
                                id="editVaultDescription" maxlength="3000" />
                        </div>
                        <div class="my-2">
                            <input v-model="editable.isPrivate" type="checkbox" class="btn-check" id="editVaultPrivate"
                                autocomplete="off" default="false">
                            <label class="btn btn-outline-warning" for="editVaultPrivate">Private</label>
                        </div>
                        <div class="text-end">
                            <button class="btn btn-success" type="submit">Create Vault</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService';

export default {
    setup() {
        const editable = ref({})
        const route = useRoute()
        return {
            editable,
            route,
            async EditVault() {
                try {
                    const editData = editable.value
                    const vaultId = route.params.vaultId
                    await vaultsService.EditVault(vaultId, editData)
                    Modal.getOrCreateInstance('#editVault').hide()
                    editable.value = {}
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>