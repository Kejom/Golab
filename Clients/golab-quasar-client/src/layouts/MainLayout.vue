<template>
  <q-layout view="lHr lpR fFf" >

    <q-header bordered class="bg-white text-black">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-toolbar-title class="text-weight-bold">
          <q-avatar class="q-pa-md lt-md header-icon">
            <img src="../assets/golab-logo.jpg">
          </q-avatar>
          <span class="gt-sm q-ml-sm"> {{ route.name }}</span>

        </q-toolbar-title>

      </q-toolbar>
    </q-header>

    <q-drawer show-if-above v-model="leftDrawerOpen" side="left" bordered>
      <q-avatar class="q-mx-sm">
        <img src="../assets/golab-logo.jpg">
      </q-avatar>
      <navigation />
    </q-drawer>

    <q-drawer show-if-above v-model="rightDrawerOpen" side="right" bordered>
    </q-drawer>

    <q-page-container class="full-height" style="max-width: 1400px; margin: 0 auto;">
      <router-view/>
    </q-page-container>

  </q-layout>
</template>

<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import Navigation from 'src/components/layout/Navigation.vue'


export default {
  components: {
    Navigation,

  },
  setup() {
    const route = useRoute()
    const leftDrawerOpen = ref(false)
    const rightDrawerOpen = ref(false)

    return {
      leftDrawerOpen,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value
      },

      rightDrawerOpen,
      toggleRightDrawer() {
        rightDrawerOpen.value = !rightDrawerOpen.value
      },
      route,
    }
  }
}
</script>

<style lang="sass">
.header-icon
  position: absolute
  bottom: 20px,
  left: 50%
  transform: translateX(-50%)
.page-container
  background-color: $accent
</style>