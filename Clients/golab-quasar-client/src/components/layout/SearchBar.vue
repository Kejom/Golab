<template>
    <q-input outlined rounded dense class="q-ma-md" :placeholder="placeholder" v-model="searchValue" @update-model-value="updateSearchValue">
    <template v-slot:prepend>
      <q-icon name="search" />
    </template>
  </q-input>
</template>

<script>
import { ref } from 'vue';
export default {
name: 'SearchBar',
props: {
placeholder: {
    type: String,
    default: "Szukaj..."
},
value: {
    type: String,
}
},
setup(props){
let searchValue = ref(props.value)

const updateSearchValue = (newValue) => {
    searchValue.value = newValue;
}
return {
    searchValue,
    updateSearchValue
}
},
watch: {
value(newValue) {
    this.searchValue = newValue
}
}

}
</script>

<style lang="sass" scoped>

</style>