<template>
  <q-page class="flex flex-center">
    <q-scroll-area class="absolute full-width full-height">
      <div v-if="userStore.loggedUserId">
        <coo-form v-if="userStore.loggedUserProfile" />
        <q-btn-group v-else spread class="q-ma-md">
          <q-btn label="Aby dodawać gruchnięcia musisz uzupełnić swój profil" color="primary" text-color="accent"
            to="/editprofile" />
        </q-btn-group>      
      </div>

      <coos-list :coos="coos" />
    </q-scroll-area>
  </q-page>
</template>

<script>
import { defineComponent, computed, onMounted, ref } from 'vue';
import CooForm from 'src/components/coo/CooForm.vue'
import CoosList from 'src/components/coo/CoosList.vue';
import { getCoos } from 'src/services/cooService'
import { useUserStore } from 'src/stores/userStore';


export default defineComponent({
  name: 'IndexPage',
  components: {
    CooForm,
    CoosList
  },
  setup() {
    const coos = ref([]);
    const userStore = useUserStore();
    onMounted(async () => {
      //var result = await api.get("/coos", {headers: {'Content-type': 'application/json'}})
      coos.value = await getCoos();
      console.log(coos.value);
    })

    return {
      coos,
      userStore
    }
  }
})
</script>
<style lang="sass">

</style>
