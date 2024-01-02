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
import { useCounterStore } from 'src/stores/example-store';
export default {
  mounted () {
    const mgr = oidc;
    const router = useRouter();
    const store = useCounterStore();
    mgr.signinRedirectCallback().then(function (user) {
        console.log(user);
        store.setLoggedUser(user);
      router.push("/");
    }).catch(function (err) {
      console.log(err)
    })
  }
}
</script>