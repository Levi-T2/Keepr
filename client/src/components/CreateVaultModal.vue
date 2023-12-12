<template>
    <div class="modal fade" id="createVault" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Create Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="CreateVault()">
                        <div class="my-2">
                            <label for="vaultName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="vaultName" required
                                maxlength="300">
                        </div>
                        <div class="my-2">
                            <label for="vaultDescription" class="form-label">Description</label>
                            <textarea v-model="editable.description" type="text" class="form-control" id="vaultDescription"
                                required maxlength="3000" />
                        </div>
                        <div class="my-2">
                            <input v-model="editable.isPrivate" type="checkbox" class="btn-check" id="vaultPrivate"
                                autocomplete="off" default="false">
                            <label class="btn btn-outline-warning" for="vaultPrivate">Private</label>
                        </div>
                        <div class="my-2">
                            <label for="vaultImg" class="form-label">Image</label>
                            <input v-model="editable.img" type="url" class="form-control" id="vaultImg" required
                                maxlength="2000">
                            <div id="keepHelp" class="form-text">This must be a url!</div>
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
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { Modal } from 'bootstrap';

export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async CreateVault() {
                try {
                    const vaultData = editable.value
                    if (vaultData.isPrivate == null) {
                        vaultData.isPrivate = false
                        await vaultsService.CreateVault(vaultData)
                        Modal.getOrCreateInstance("#createVault").hide()
                        editable.value = {}
                    } else {
                        await vaultsService.CreateVault(vaultData)
                        Modal.getOrCreateInstance("#createVault").hide()
                        editable.value = {}
                    }
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>