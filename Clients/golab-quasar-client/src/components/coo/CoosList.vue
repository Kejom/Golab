<template>
  <q-separator class="divider" size="10px" color="accent" />
  <q-list>
    <transition-group appear enter-active-class="animated fadeIn slow" leave-active-class="animated fadeOut slow">
      <coo v-for="coo in coos" :key="coo.id" :coo="coo" class="coo" />
    </transition-group>
  </q-list>
</template>
  
<script>
import Coo from './Coo.vue'

export default {
  components: {
    Coo
  },
  props: {
    coos: {
      type: Array,
      required: true,
      default: () => [],
    }
  },
  name: 'CoosList',
  setup() {
    return {
    }
  }
}
</script>
  
<style lang="sass">
.coo:not(:first-child)
  border-top: 5px solid $accent
  </style>