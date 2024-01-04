<template>
    <q-item>
        <q-item-section avatar>
            <q-avatar v-if="user.avatarId">
                <img :src="`http://localhost:6001/files/avatar/${user.avatarId}`">
            </q-avatar>
        </q-item-section>

        <q-item-section>
            <q-item-label lines="1">{{ user.displayName }} <span class="text-grey-7">@{{ user.handle }}</span></q-item-label>
            <q-item-label>

                {{ comment.content }}
            </q-item-label>
        </q-item-section>

        <q-item-section side top>
            <q-item-label>{{ relativeDate(comment.created) }} temu</q-item-label>
            <q-btn flat round color="grey" :icon="comment.userId === loggedUserId ? 'fas fa-trash' : ''" size="sm"
                @click="removeClicked" :disable="comment.userId !== loggedUserId" />
        </q-item-section>
    </q-item>
</template>
  
<script>
import { onMounted, ref } from 'vue';
import { formatDistance } from 'date-fns';
import { pl } from 'date-fns/locale';
import { useUserStore } from 'src/stores/userStore';
export default {
    name: "Comment",
    props: {
        comment: {
            type: Object,
            required: true,
            default: () => { },
        }
    },
    setup(props, { emit }) {
        const userStore = useUserStore();
        const user = ref({});
        const loggedUserId = userStore.loggedUserId
        const relativeDate = (value) => formatDistance(value, Date.now(), { locale: pl });

        const removeClicked = () => emit("removeClicked", props.comment.id);


        onMounted(async () => {
            const userData = await userStore.getUser(props.comment.userId);
            user.value = userData;
        })

        return {
            user,
            userStore,
            loggedUserId,
            relativeDate,
            removeClicked
        }
    }
}
</script>
  
<style lang="sass" scoped>
  
  </style>