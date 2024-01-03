<template>
  <router-view />
</template>

<script>
import { defineComponent, onMounted } from 'vue'
import { useCooStore } from './stores/cooStore';
import { useUserStore } from './stores/userStore';
import { getCurrentUserProfile } from 'src/services/profileService'
import { getLikes } from 'src/services/likeService';
import mgr from 'src/oidc/oidc-lite';

export default defineComponent({
  name: 'App',
  setup() {
    const cooStore = useCooStore();
    const userStore = useUserStore();

    const initUser = async () => {
      const user = await mgr.getUser();

      if (!user) return;

      userStore.setLoggeduserId(user.profile.sub);
      const profile = await getCurrentUserProfile();
      if (profile)
      userStore.setLoggedUserProfile(profile);

      const likes = await getLikes();

      if (likes)
      userStore.setLoggedUserLikes(likes);
    }
    onMounted(async () => {
      await initUser();
      await cooStore.initCoos();
    })
  }
})
</script>
