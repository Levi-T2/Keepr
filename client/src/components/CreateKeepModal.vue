<template>
    <div class="modal fade" id="createKeep" tabindex="-1" aria-labelledby="CreateKeepModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Create Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="CreateKeep()">
                        <div class="my-2">
                            <label for="keepName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="keepName" required
                                maxlength="300">
                        </div>
                        <div class="my-2">
                            <label for="keepDescription" class="form-label">Description</label>
                            <textarea v-model="editable.description" type="text" class="form-control" id="keepDescription"
                                required :maxlength="3000" />
                        </div>
                        <div class="my-2">
                            <label for="keepImg" class="form-label">Image</label>
                            <input v-model="editable.img" type="url" class="form-control" id="keepImg" required
                                maxlength="2000">
                            <div id="keepHelp" class="form-text">This must be a url!</div>
                        </div>
                        <div class="text-end">
                            <button class="btn btn-success" type="submit">Create Keep</button>
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
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import masonry from 'masonry-layout';

export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async CreateKeep() {
                try {
                    const keepData = editable.value
                    await keepsService.CreateKeep(keepData)
                    Modal.getOrCreateInstance("#createKeep").hide()
                    editable.value = {}
                    // NOTE this needs to be ran to redo the masonry when data is added.
                    let row = document.querySelector("[data-masonry]")
                    new masonry(row, { percentPosition: true, })
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>