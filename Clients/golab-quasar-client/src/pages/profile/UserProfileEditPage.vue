<template>
    <user-profile-form />
</template>

<script>


import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import mgr from '../../oidc/oidc-lite'
import UserProfileForm from 'src/components/profile/UserProfileForm.vue'
export default {
    components: {
        UserProfileForm
    },
    name: 'UserProfileEditPage',
    setup() {
        const router = useRouter();

        onMounted(async() => {
            const user = await mgr.getUser();
            if(!user)
                router.push("/");
        })
    }
}
</script>

<style lang="sass" scoped>

</style>