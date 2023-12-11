<template>
    <div class="modal fade" id="keepDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div v-if="activeKeep" class="container-fluid">
                        <section class="row">
                            <div class="col-5">
                                <img :src="activeKeep.img" alt="Keep Image" class="keep-img">
                            </div>
                            <div class="col-7">
                                <div>
                                    <p>{{ activeKeep.name }}</p>
                                    <p>{{ activeKeep.description }}</p>
                                </div>
                                <div>
                                    <button class="btn btn-primary">Save</button>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {
            activeKeep: computed(() => AppState.activeKeep)
        }
    }
};
</script>


<style lang="scss" scoped>
.keep-img {
    height: auto;
    width: 16rem;
    background-position: center;
    background-size: cover;
}
</style>