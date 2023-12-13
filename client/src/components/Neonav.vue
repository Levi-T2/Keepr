<template>
    <nav class="nav-style">
        <div class="container-fluid">
            <section class="row align-items-center">
                <div class="col-1">
                    <RouterLink :to="{ name: 'Home' }" title="To Home Page">
                        <p type="button" role="button" title="To Home Page" class="mb-0 home-btn"><i
                                class="mdi mdi-home"></i></p>
                    </RouterLink>
                </div>
                <div class="col-1">
                    <div v-if="Object.keys(account).length" class="dropdown">
                        <button title="See Create Options" class="btn create-btn dropdown-toggle" type="button"
                            data-bs-toggle="dropdown" aria-expanded="false">
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
                        <img src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678129-lock-512.png"
                            alt="Keepr Image" class="keepr-img">
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
    text-shadow: 2.25px 2px 1.25px rgba(255, 255, 255, 0.71);
}

.create-btn {
    background-color: lightgoldenrodyellow;
    border: 2px solid rgba(0, 0, 0, 0);
    transition: ease-in-out 0.255s;
    border-radius: 20px;
}

.create-btn:hover {
    border: 2px solid gainsboro;
    background-color: rgb(36, 36, 34);
    color: white;
}

.keepr-img {
    width: 1.725em;
    height: 1.725em;
}

.home-btn {
    background-color: lightgoldenrodyellow;
    color: black;
    text-align: center;
    padding: 0.25rem;
    border-radius: 20px;
    font-size: 1.25em;
    transition: ease-in-out 0.225s;
}

.home-btn:hover {
    background-color: rgb(36, 36, 34);
    color: white;
}

.nav-style {
    background: rgb(238, 174, 202);
    background: radial-gradient(circle, rgba(238, 174, 202, 1) 0%, rgba(158, 148, 233, 1) 100%);
    border-bottom: solid 2px black;
}
</style>