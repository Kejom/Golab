<template>
  <q-list>
    <q-item clickable v-ripple to="/" exact>
      <q-item-section avatar>
        <q-icon name="home" size="md" />
      </q-item-section>

      <q-item-section class="text-h6 text-weight-bold">Strona Główna</q-item-section>
    </q-item>

    <q-item clickable v-ripple to="/about" exact>
      <q-item-section avatar>
        <q-icon name="help" size="md" />
      </q-item-section>

      <q-item-section class="text-h6 text-weight-bold">Informacje o Aplikacji</q-item-section>
    </q-item>

    <q-item  v-if="store.loggedUserId" clickable v-ripple to="/editprofile" exact>
          <q-item-section avatar>
            <q-icon name="manage_accounts" size="md"  />
          </q-item-section>

          <q-item-section class="text-h6 text-weight-bold">Edytuj Profil</q-item-section>
        </q-item>

    <q-item v-if="store.loggedUserId" clickable v-ripple @click="onLogout" exact>
      <q-item-section avatar>
        <q-icon name="logout" size="md" />
      </q-item-section>

      <q-item-section class="text-h6 text-weight-bold">Wyloguj</q-item-section>
    </q-item>

    <q-item v-else clickable v-ripple @click="onLogin" exact>
      <q-item-section avatar>
        <q-icon name="login" size="md" />
      </q-item-section>

      <q-item-section class="text-h6 text-weight-bold">Zaloguj / Zarejestruj</q-item-section>
    </q-item>






  </q-list>
</template>
  
<script>
import { useRouter } from 'vue-router';
import mgr from 'src/oidc/oidc-lite';
import { useUserStore } from 'src/stores/userStore'


export default {
  name: 'Navigation',
  setup() {
    const router = useRouter();
    const store = useUserStore();



    const onLogin = () => mgr.signinRedirect();
    const onLogout = () => mgr.signoutRedirect();

    return {
      onLogin,
      onLogout,
      store,
    }
  }
}
</script>
  
<style lang="sass">
  
  </style>