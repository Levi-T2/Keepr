<template>
    <div class="modal fade" id="editAccount" tabindex="-1" aria-labelledby="EditAccountModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Edit Account</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="EditAccount()">
                        <div class="my-1">
                            <label for="accountName" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="accountName"
                                maxlength="300">
                        </div>
                        <div class="my-1">
                            <label for="accountBio" class="form-label">Bio</label>
                            <textarea v-model="editable.bio" type="text" class="form-control" id="accountBio"
                                maxlength="3000" />
                        </div>
                        <div class="my-1">
                            <label for="accountPicture" class="form-label">Profile Picture</label>
                            <input v-model="editable.picture" type="url" class="form-control" id="accountPicture"
                                maxlength="255">
                        </div>
                        <div class="my-1">
                            <label for="accountCoverImg" class="form-label">Cover Img</label>
                            <input v-model="editable.coverImg" type="url" class="form-control" id="accountCoverImg"
                                maxlength="2000">
                        </div>
                        <div class="my-1">
                            <button type="submit" class="btn btn-warning">Save <i
                                    class="mdi mdi-content-save-plus"></i></button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
import { Modal } from 'bootstrap';

export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async EditAccount() {
                try {
                    const editData = editable.value
                    await accountService.EditAccount(editData)
                    Modal.getOrCreateInstance('#editAccount').hide()
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