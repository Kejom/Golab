<template>
    <div class="q-py-lg q-px-md row items-end q-col-gutter-md">
      <div class="col">
          <q-input class=".new-comment" bottom-slots v-model="newCommentText" placeholder="Dodaj Komentarz..."
              counter maxlength="280" autogrow>

          </q-input>
      </div>
      <div class="col col-shrink">
          <q-btn unelevated rounded color="primary" label="Dodaj Komentarz" no-caps :disable="!newCommentText" class="q-mb-lg"
              @click="addNewComment" />
      </div>
  </div>
</template>

<script>
import {ref} from 'vue'
export default {
name: 'CommentForm',
setup(_, {emit}){
  const newCommentText = ref("");

  const addNewComment = () => {
      emit("addClicked", {commentText: newCommentText.value});
      newCommentText.value = "";
  }

  return {
      newCommentText,
      addNewComment
  }
}
}
</script>

<style lang="sass" scoped>
.new-comment
  textarea
    font-size: 19px
    line-height: 1.4 !important
</style>