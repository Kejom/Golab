<template>
    <q-page class="flex flex-center">
        <q-scroll-area class="absolute full-width full-height">
            <q-card flat v-if="coo && user">
                <q-card-section horizontal>
                    <q-card class="col" flat>
                        <q-item>
                            <q-item-section avatar>
                                <q-avatar v-if="user.avatarId" size="4em">
                                    <img :src="`http://localhost:6001/files/avatar/${user.avatarId}`">
                                </q-avatar>
                            </q-item-section>

                            <q-item-section>
                                <q-item-label><span class="text-subtitle1">{{ user.displayName }}</span> <br class="lt-md">&bull;
                                    {{ relativeDate(coo.created) }} temu</q-item-label>
                                <q-item-label caption>
                                    @{{ user.handle }}
                                </q-item-label>
                            </q-item-section>
                        </q-item>

                        <q-separator />

                        <q-card-section horizontal>
                            <q-card-section>
                                {{ coo.content }}
                            </q-card-section>



                        </q-card-section>
                    </q-card>

                    <q-card-actions vertical class="justify-around q-px-md">

                        <q-btn flat round :color="isLiked ? 'pink' : 'grey'"
                            :icon="isLiked ? 'fas fa-heart' : 'far fa-heart'" size="md" :label="coo.likes"
                            text-color="primary" />
                        <q-btn v-if="userStore.loggedUserId === coo.userId" flat round color="grey" icon="fas fa-trash"
                            size="md" text-color="primary" />
                    </q-card-actions>
                </q-card-section>
            </q-card>
            <q-separator class="divider" size="5px" color="accent" />
            <div v-if="userStore.loggedUserId">
                <comment-form v-if="userStore.loggedUserProfile" @addClicked="addComment" />
                <q-btn-group v-else spread class="q-ma-md">
                    <q-btn label="Aby dodawać komentarze musisz uzupełnić swój profil" color="primary" text-color="accent"
                        to="/editprofile" />
                </q-btn-group>
            </div>

            <q-separator class="divider" size="5px" color="accent" />
            <comment-list :comments="comments" @commentRemoved="removeComment" />
        </q-scroll-area>
    </q-page>
</template>

<script>
import { ref, onMounted, onUnmounted, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { formatDistance } from 'date-fns'
import { pl } from 'date-fns/locale'
import CommentList from 'src/components/comments/CommentList.vue'
import CommentForm from 'src/components/comments/CommentForm.vue'
import { useUserStore } from 'src/stores/userStore'
import { useCooStore } from 'src/stores/cooStore'
import { getCooById } from 'src/services/cooService'
import { getComments, postComment, deleteComment } from 'src/services/commentsService'

export default {
    name: 'CooPage',
    components: {
        CommentForm,
        CommentList,
    },
    setup() {
        const route = useRoute();
        const userStore = useUserStore();
        const cooStore = useCooStore();
        const coo = computed(() => cooStore.getCooById(route.params.cooId))
        const user = ref({});
        const comments = ref([]);
        const isLiked = computed(() => userStore.loggedUserLikes.has(route.params.cooId))

        watch(coo, () => {
            init()
        })

        const init = async () => {
            const cooData = await getCooById(route.params.cooId)
            if (!cooData)
                return;
            const userData = await userStore.getUser(cooData.userId);

            if (userData)
                user.value = userData;

            const commentsData = await getComments(route.params.cooId);
            if (commentsData)
                comments.value = commentsData;
        }

        const addComment = async ({ commentText }) => {
            var newComment = {
                cooId: route.params.cooId,
                content: commentText,
            }

            var result = await postComment(newComment);

            if (!result) return;

            comments.value.unshift(result);
            cooStore.incrementCooComments(route.params.cooId, 1);
        }

        const removeComment = async (id) => {
            var success = await deleteComment(id);

            if (!success) return;

            comments.value = comments.value.filter(c => c.id !== id);
            cooStore.incrementCooComments(props.cooId, -1);
        }



        const relativeDate = (value) => formatDistance(value, Date.now(), { locale: pl })



        onMounted(async () => {
            init()
        })



        return {
            coo,
            user,
            comments,
            userStore,
            isLiked,
            addComment,
            removeComment,
            relativeDate,
        }
    }
}
</script>

<style></style>