<template>
    <div class="col-12 vault-style mt-2">
        <div class="txt-bg">
            <p class="fw-bold mb-0">{{ vault.name }}</p>
            <p class="mb-0">by: {{ vault.creator.name }}</p>
        </div>
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
        }
    }
};
</script>


<style lang="scss" scoped>
.vault-style {
    background-image: v-bind(vaultImg);
    height: 16rem;
    width: 70%;
    background-position: center;
    background-size: cover;
    border-radius: 10px;
    display: flex;
    justify-content: center;
    align-items: flex-end;
}

.txt-bg {
    background-color: rgba(30, 30, 30, 0.18);
    backdrop-filter: blur(12px);
    color: white;
    padding: 0.25rem;
    border-top-right-radius: 15px;
    border-top-left-radius: 15px;
    font-size: 1.25em;
    text-align: center;
}
</style>