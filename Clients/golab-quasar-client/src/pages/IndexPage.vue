<template>
  <q-page class="flex flex-center">
    <q-scroll-area class="absolute full-width full-height">
      <coo-form  />
    </q-scroll-area>
  </q-page>
</template>

<script>
import { defineComponent, computed } from 'vue';
import CooForm from 'src/components/coo/CooForm.vue'


export default defineComponent({
  name: 'IndexPage',
  components: {
    CooForm,
  },
  setup() {



    return {
    }
  }
})
</script>
<style lang="sass">

</style>
