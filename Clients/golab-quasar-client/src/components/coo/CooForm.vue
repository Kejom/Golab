<template>
    <div class="q-py-lg q-px-md row items-end q-col-gutter-md">
        <div class="col">
            <q-input class="new-coo" bottom-slots v-model="newCooText" placeholder="Podziel się czymś ciekawym."
                counter maxlength="280" autogrow>
                <template v-slot:before>
                    <q-avatar v-if="store.loggedUserProfile" size="4em" class="gt-sm">
                        <img :src="`http://localhost:6001/files/avatar/${store.loggedUserProfile.avatarId}`">
                    </q-avatar>
                </template>
            </q-input>
        </div>
        <div class="col col-shrink">
            <q-btn unelevated rounded color="primary" label="Gruchnij" no-caps :disable="!newCooText" class="q-mb-lg"
                @click="addNewCoo" />
        </div>
    </div>
</template>

<script>
import {ref} from 'vue'
import { api } from 'src/boot/axios';
import mgr from 'src/oidc/oidc-lite';
import { useUserStore } from 'src/stores/userStore';
import { useCooStore } from 'src/stores/cooStore';
import { postCoo } from 'src/services/cooService';

export default {
    name: 'CooForm',
    setup(_, {emit}){
        const newCooText = ref('');
        const store = useUserStore();
        const cooStore = useCooStore();
        const addNewCoo = async() => {           
            var coo = {
                content: newCooText.value,
            }
            var result = await postCoo(coo);
            if(result)
                cooStore.addCoo(result);
            newCooText.value = "";
        }

        return {
            newCooText,
            addNewCoo,
            store,

        }
    }
}
</script>

<style lang="sass">
.new-coo
  textarea
    font-size: 19px
    line-height: 1.4 !important
</style>