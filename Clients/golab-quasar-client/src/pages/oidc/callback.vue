<template>
    <q-page>
      <div class="fixed-center text-center">
        <p>
          <q-icon name="person" size="8em" color="grey-5" />
        </p>
        <p class="text-faded">We're preparing your session.</p>
        <p class="text-faded"><strong>Please wait while we set up your information...</strong></p>
      </div>
    </q-page>
</template>

<script>
// import Oidc from 'oidc-client'
import oidc from '../../oidc/oidc'
import { useRouter } from 'vue-router';
import { useUserStore } from 'src/stores/userStore';
import { getCurrentUserProfile } from 'src/services/profileService';
export default {
  mounted () {
    const mgr = oidc;
    const router = useRouter();
    const store = useUserStore();
    mgr.signinRedirectCallback().then(function (user) {
        store.setLoggeduserId(user.profile.sub);
        getCurrentUserProfile().then(function(user){
          if(user)
            store.setLoggedUserProfile(user);
        })
      router.push("/");
    }).catch(function (err) {
      console.log(err)
    })
  }
}
</script>