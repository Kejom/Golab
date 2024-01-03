<template>
    <div class="q-py-lg q-px-md row items-end q-col-gutter-md">
        <div class="col">
            <q-input class="new-coo" bottom-slots v-model="newCooText" placeholder="Podziel się czymś ciekawym."
                counter maxlength="280" autogrow>
                <template v-slot:before>
                    <q-avatar size="4em" class="gt-sm">
                        <img src="../../assets/golab-default-avatar.jpg">
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
import { useCounterStore } from 'src/stores/example-store';
import { postCoo } from 'src/services/cooService';

export default {
    name: 'CooForm',
    setup(_, {emit}){
        const newCooText = ref('');
        const store = useCounterStore();
        const addNewCoo = async() => {
            //emit('addClicked', {cooText: newCooText.value})
            console.log(newCooText.value);
            
            var coo = {
                content: newCooText.value,
            }

            //var token = store.loggedUser.access_token;
            //console.log(token);
            //var result = await api.post('/coos', coo, {headers: {"Authorization": `Bearer ${token}`, 'Content-type': 'application/json'} })
            //console.log(result);
            await postCoo(coo);
            newCooText.value = "";
        }

        return {
            newCooText,
            addNewCoo,

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