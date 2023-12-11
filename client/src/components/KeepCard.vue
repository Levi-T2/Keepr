<template>
    <section class="row">
        <div role="button" @click="OpenKeepModal(keep)" class="col-12 p-2">
            <div class="img-container keep-card">
                <img :src="keep.img" alt="Keep Image" class="img-fluid rounded">
                <div class="bottom-left">
                    <p class="txt-bg">{{ keep.name }}</p>
                </div>
                <div class="bottom-right">
                    <img :src="keep.creator.picture" alt="Creator Avatar" class="creator-img">
                </div>
            </div>

        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {
        return {
            OpenKeepModal(keepData) {
                try {
                    AppState.activeKeep = null
                    AppState.activeKeep = keepData
                    Modal.getOrCreateInstance("#keepDetails").show()
                } catch (error) {
                    Pop.error(error)
                }
            }
            // keepCreatorImg: computed(() => `url(${props.keep.creator.picture})`)
        }
    }
};
</script>


<style lang="scss" scoped>
// .keep-img {
//     height: auto;
//     width: 22.5em;
//     background-position: center;
//     background-size: cover;
//     border-radius: 7px;
// }

.keep-card {
    box-shadow: 1px 2px 6px 4px rgb(133, 133, 133);
    border-radius: 6px;
    transition: ease-in-out 0.275s;
}

.keep-card:hover {
    transform: translateY(0.25em);
    box-shadow: 1px 2px 6px 4px rgb(67, 67, 67);
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
    bottom: -5px;
    left: 12px;
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;
}

// !SECTION
</style>