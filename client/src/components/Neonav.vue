<template>
    <nav class="nav-style">
        <div class="container-fluid">
            <section class="row align-items-center">
                <div class="col-1">
                    <RouterLink :to="{ name: 'Home' }" title="To Home Page">
                        <p type="button" role="button" class="mb-0 home-btn"><i class="mdi mdi-home"></i></p>
                    </RouterLink>
                </div>
                <div v-if="Object.keys(account).length" class="col-1">
                    <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                            aria-expanded="false">
                            Create
                        </button>
                        <ul class="dropdown-menu">
                            <li data-bs-toggle="modal" data-bs-target="#createKeep" role="button"><a
                                    class="dropdown-item">Create Keep</a></li>
                            <li data-bs-toggle="modal" data-bs-target="#createVault" role="button"><a
                                    class="dropdown-item">Create Vault</a></li>
                        </ul>
                    </div>
                </div>
                <div class="col-8 text-center">
                    <div class="d-flex justify-content-center">
                        <img src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678129-lock-512.png" alt=""
                            class="keepr-img">
                        <p class="keepr-txt">Keepr</p>
                    </div>
                </div>
                <div class="col-2">
                    <div id="loginComp" class="d-flex justify-content-end">
                        <Login />
                    </div>
                </div>
            </section>
        </div>
    </nav>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';
import Login from './Login.vue';

export default {
    setup() {
        return {
            account: computed(() => AppState.account)
        };
    },
    components: { Login }
};
</script>


<style lang="scss" scoped>
.keepr-txt {
    font-family: 'Kanit', sans-serif;
    margin-bottom: 0%;
    font-size: 1.375em;
}

.keepr-img {
    width: 1.725em;
    height: 1.725em;
}

.home-btn {
    background-color: rgb(220, 210, 190);
    color: black;
    text-align: center;
    padding: 0.25rem;
    border-radius: 20px;
    font-size: 1.25em;
    transition: ease-in-out 0.225s;
}

.home-btn:hover {
    background-color: rgb(156, 144, 122);
    color: white;
}

.nav-style {
    background-color: rgb(244, 235, 217);
    border-bottom: solid 2px black;
}
</style>