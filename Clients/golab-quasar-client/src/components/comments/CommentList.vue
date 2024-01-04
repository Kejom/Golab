<template>
    <q-list>
        <transition-group appear enter-active-class="animated fadeIn slow" leave-active-class="animated fadeOut slow">
            <comment v-for="comment in comments" :key="comment.id" :comment="comment" class="comment"
                @removeClicked="deleteComment" />
        </transition-group>
    </q-list>
</template>

<script>
import { ref, onMounted } from 'vue'
import Comment from './Comment.vue';
import { getComments, postComment, deleteComment } from '../../services/commentsService'
import { useCooStore } from 'src/stores/cooStore';

export default {
    name: "CommentList",
    components: {
        Comment
    },
    props: {
        comments: {
            type: Array,
            required: true,
            default: () => [],
        }
    },
    setup(props, { emit }) {

        const cooStore = useCooStore();



        const addComment = async(commentText) => {

        }

        const deleteComment = (id) => {
            emit("commentRemoved", id);
        }

        return {
            addComment,
            deleteComment,
        }
    }
}
</script>

<style lang="sass" scoped>
.comment:not(:first-child)
  border-top: 5px solid $accent
</style>