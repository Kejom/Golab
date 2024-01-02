<template>
    <div class="q-pa-md" style="max-width: 400px; margin: 0 auto;">

        <q-form @submit="onSubmit" class="q-gutter-md">
            <q-input filled type="text" v-model="email" label="Email" lazy-rules
                :rules="[val => val && val.length > 0 || 'Email nie może być pusty!']" />

            <q-input filled type="password" v-model="password" label="Hasło" lazy-rules
                :rules="[val => val && val.length > 0 || 'Hasło nie może być puste!']" />


            <div class="column">
                <q-btn label="Zaloguj" type="submit" color="primary" text-color="accent" class="q-ma-sm" />
                <q-btn label="Zarejestruj Nowe Konto" color="accent" text-color="primary" to="/register" class="q-ma-sm" />



            </div>
        </q-form>

    </div>
</template>

<script>
import { ref } from 'vue'


export default {
    name: 'LoginForm',
    setup() {
        const email = ref("");
        const password = ref("");
        const onSubmit = async () => {

        }


        return {
            email,
            password,
            onSubmit,
        }
    }
}
</script>

<style lang="sass" scoped>

</style>