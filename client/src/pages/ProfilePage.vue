<template>
    <div v-if="profile">
        <AccountDetails :account="profile" />
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { userService } from '../services/UserService';
import { useRoute } from 'vue-router';
import AccountDetails from '../components/AccountDetails.vue';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            GetProfileDetails();
        });
        async function GetProfileDetails() {
            try {
                const profileId = route.params.profileId;
                await userService.GetProfileDetails(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            profile: computed(() => AppState.profile),
        };
    },
    components: { AccountDetails }
};
</script>


<style lang="scss" scoped></style>