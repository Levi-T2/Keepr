<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-12 account-style">
                <div class="img-container">
                    <img src="https://images.unsplash.com/photo-1500964757637-c85e8a162699?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8Y292ZXIlMjBpbWFnZXxlbnwwfHwwfHx8MA%3D%3D"
                        alt="Cover Image" class="cover-img">
                    <div class="img-position">
                        <img :src="account.picture" alt="Account Picture" class="account-img">
                    </div>
                </div>

                <div class="col-12 mt-5 p-0">
                    <div class="text-center">
                        <p>{{ account.name }}</p>
                        <p>Email: {{ account.email }}</p>
                    </div>
                </div>

            </div>
            <section v-if="vaults.length" class="row">
                <div class="text-center">
                    <p class="mb-0 fs-3">Vaults</p>
                </div>
                <div v-for="vault in vaults" :key="vault.id" class="col-12 col-md-4 col-lg-3 mt-2">
                    <AccountVaults :vault="vault" />
                </div>
            </section>
            <section v-else class="row">
                <div class="col-12">
                    <div class="text-center mt-1">
                        <p class="mb-0 fs-3">Loading ...</p>
                    </div>
                </div>
            </section>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';
import { Account } from '../models/Account';
import AccountVaults from '../components/AccountVaults.vue'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop';

export default {
    props: {
        account: { type: Account, required: true }
    },
    setup() {
        onMounted(() => {
            GetVaultsForAccount();
        })
        async function GetVaultsForAccount() {
            try {
                await vaultsService.GetVaultsForAccount()
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            vaults: computed(() => AppState.vaults),
        }
    },
    components: { AccountVaults }
};
</script>


<style lang="scss" scoped>
.cover-img {
    height: 18rem;
    width: 100%;
    background-position: center;
    background-size: cover;
    border-radius: 10px;
}

.account-img {
    background-position: center;
    height: 6rem;
    width: 6rem;
    border-radius: 22px;
}

.account-style {
    padding: 1%;
}

// SECTION Css for cover img position

.img-container {
    position: relative;
    text-align: center;
}

.img-position {
    position: absolute;
    top: 100%;
    left: 50%;
    transform: translate(-50%, -50%);
}

// !SECTION
</style>