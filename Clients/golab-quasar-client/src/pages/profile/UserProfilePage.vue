<template>
    <q-page class="flex flex-center">
        <q-scroll-area class="absolute full-width full-height">
            <user-profile v-if="user" :user="user" />
            <coos-list v-if="user" :coos="cooStore.getCoosByUserId(user.id)" />
        </q-scroll-area>
    </q-page>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router'
import { useCooStore } from 'src/stores/cooStore';
import UserProfile from 'src/components/profile/UserProfile.vue';
import CoosList from 'src/components/coo/CoosList.vue';
import { getUserProfileByHandle } from 'src/services/profileService';

export default {
    components: {
        UserProfile,
        CoosList
    },
    name: 'UserProfilePage',
    setup() {
        const route = useRoute();
        const router = useRouter();
        const cooStore = useCooStore();
        const user = ref(null);

        onMounted(async () => {
            const userData = await getUserProfileByHandle(route.params.userHandle);

            if (!userData)
                router.go(-1);
            user.value = userData;

        })

        return {
            user,
            cooStore
        }

    }
}
</script>

<style lang="sass" scoped>

</style>