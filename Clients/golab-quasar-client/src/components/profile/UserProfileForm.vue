<template>
    <div class="q-pa-md" style="max-width: 400px; margin: 0 auto;">
        <q-form @submit="onSubmit" class="q-gutter-md">
            <q-input filled type="text" v-model="formData.profileName" label="Nazwa Profilu" lazy-rules
                :rules="[val => val && val.length > 0 || 'Nazwa użytkownika nie może być pusta!']" />
            <q-input filled type="text" v-model="formData.handle" label="Identyfikator Użytkownika" lazy-rules disable />
            <q-input v-model="formData.profileDescription" label="Opis Profilu" filled autogrow />
            <div class="row">
                <div class="col">
                    <q-avatar v-if="formData.avatarPreview" size="5rem">
                        <img :src="formData.avatarPreview">
                    </q-avatar>
                </div>
                <div class="col-8">

                    <q-file v-model="formData.avatar" label="Dodaj zdjecie profilowe" @update:model-value="updatePreview" />
                </div>
            </div>
            <q-btn-group spread class="q-ma-md">
                <q-btn label="Zapisz Zmiany" type="submit" color="primary" text-color="accent" />
            </q-btn-group>
        </q-form>
    </div>
</template>

<script>
import { reactive, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router';
import { useUserStore } from 'src/stores/userStore';
import mgr from 'src/oidc/oidc-lite';
import { getCurrentUserProfile, postUserProfile, putUserProfile } from 'src/services/profileService';
import { postAvatarFile} from 'src/services/fileService'
export default {
    name: 'UserProfileForm',
    setup() {
        const router = useRouter();
        const userStore = useUserStore();
        const editMode = ref(false);

        const formData = reactive({
            profileName: '',
            handle: '',
            profileDescription: '',
            avatar: null,
            avatarPreview: null
        })

        onMounted(async () => {
            const user = await mgr.getUser();
            console.log(user);
            if (userStore.loggedUserProfile) {
                formData.profileName = userStore.loggedUserProfile.displayName;
                formData.handle = userStore.loggedUserProfile.handle;
                formData.profileDescription = userStore.loggedUserProfile.description;

                if (userStore.loggedUserProfile.avatarId)
                    formData.avatarPreview = `http://localhost:6001/files/avatar/${userStore.loggedUserProfile.avatarId}`;
                editMode.value = true;
            }
            else {
                formData.handle = user.profile.username
            }
        })




        const updatePreview = (val) => {
            formData.avatarPreview = URL.createObjectURL(val);
        }

        const onSubmit = async () => {
           
            
            if (editMode.value)
                await onEdit();
            else
                await onCreate();

            if(formData.avatar)
                await postAvatarFile(formData.avatar)

            var updatedUser = await getCurrentUserProfile();
            if(!updatedUser) return;

            userStore.setLoggedUserProfile(updatedUser);
            userStore.updateUserCache(updatedUser);

            router.push("/");
        }

        const onEdit = async () => {    
            var userProfile = userStore.loggedUserProfile;
            userProfile.description = formData.profileDescription;
            userProfile.displayName = formData.profileName
            await putUserProfile(userProfile);         
        }

        const onCreate = async () => {
            const userId = userStore.loggedUserId;
            var userProfile = {
                id: userId,
                description: formData.profileDescription,
                displayName: formData.profileName,
                handle: formData.handle,
                avatarId: ""
            };
            console.log(userProfile);
            await postUserProfile(userProfile);
        }


        return {
            formData,
            updatePreview,
            onSubmit,
            userStore,
            editMode
        }
    }
}
</script>

<style lang="sass" scoped>

</style>