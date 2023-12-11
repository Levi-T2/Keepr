<template>
    <nav class="nav-style">
        <div class="container-fluid">
            <section class="row align-items-center">
                <div class="col-2">
                    <RouterLink :to="{ name: 'Home' }">
                        <p type="button" role="button" class="mb-0">Home</p>
                    </RouterLink>
                </div>
                <div class="col-2">
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
                <div class="col-6 text-center">
                    <p class="mb-0">Keepr</p>
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
import { ref } from 'vue';
import Login from './Login.vue';

export default {
    setup() {
        return {
        };
    },
    components: { Login }
};
</script>


<style lang="scss" scoped>
.nav-style {
    background-color: wheat;
    border-bottom: solid 2px black;
}
</style>