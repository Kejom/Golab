<template>
  <q-item class="qweet q-py-md">
    <q-item-section avatar top>
      <q-btn v-if="user" flat :to="`/profile/${user.handle}`">
        <q-avatar v-if="user?.avatarId" size="xl">
          <img :src="`http://localhost:6001/files/avatar/${user.avatarId}`">
        </q-avatar>
        <q-avatar v-else size="xl">
          <img src="../../assets/golab-default-avatar.jpg">
        </q-avatar>
      </q-btn>
    </q-item-section>
    <q-item-section>
      <q-item-label class="text-subtitle1">
        <strong>{{ user ? user.displayName : 'nieznany' }}</strong>
        <span class="text-grey-7"> @{{ user ? user.handle : "nieznany" }}
          <br class="lt-md">&bull; {{ relativeDate(coo.created) }} temu
        </span>
      </q-item-label>

      <q-item-label class="qweet-content text-body1">
        {{ coo.content }}
      </q-item-label>


      <div class="qweet-icons row justify-around q-mt-md">
        <q-btn flat round color="grey" icon="far fa-comment" size="sm" :to="`/${coo.id}`" :label="coo.comments"
          text-color="primary" />
        <q-btn flat round color="grey" icon="fas fa-retweet" size="sm" text-color="primary" />
        <q-btn flat round :color="isLiked ? 'pink' : 'grey'" :icon="isLiked ? 'fas fa-heart' : 'far fa-heart'" size="sm"
          @click="likeClicked" :label="coo.likes" text-color="primary" :disable="!userStore.loggedUserId" />
        <q-btn flat round color="grey" :icon="userStore.loggedUserId === coo.userId ? 'fas fa-trash' : ''" size="sm"
          @click="removeClicked" :disable="userStore.loggedUserId !== coo.userId" text-color="primary" />
      </div>
    </q-item-section>

  </q-item>
</template>
  
<script>
import { ref, onMounted, computed } from 'vue'
import { formatDistance } from 'date-fns'
import { pl } from 'date-fns/locale'
import { useUserStore } from 'src/stores/userStore'
import { useCooStore } from 'src/stores/cooStore'
import { deleteCoo } from 'src/services/cooService'
import {
  likeCoo, unlikeCoo
} from 'src/services/likeService'
export default {
  name: 'Coo',
  props: {
    coo: {
      type: Object,
      required: true,
      default: () => { },
    }
  },
  setup(props, { emit }) {
    const userStore = useUserStore();
    const cooStore = useCooStore();
    const user = ref({})
    const isLiked = computed(() => userStore.loggedUserLikes.has(props.coo.id))
    
    const removeClicked = async () => {
      var success = await deleteCoo(props.coo.id);
      if (success)
        cooStore.removeCoo(props.coo.id);
    };


    const like = async () => {
      var success = await likeCoo(props.coo.id);

      if (!success) return;

      var coo = props.coo;
      coo.likes++;
      cooStore.editCoo(coo);
      userStore.addLike(props.coo.id);

    }

    const unLike = async () => {
      var success = await unlikeCoo(props.coo.id);

      if (!success) return;

      var coo = props.coo;
      coo.likes--;
      cooStore.editCoo(coo);
      userStore.removeLike(props.coo.id);
    }

    const likeClicked = async () => {
      if (isLiked.value)
        await unLike();
      else
        await like();
    }

    const relativeDate = (value) => formatDistance(value, Date.now(), { locale: pl })

    onMounted(async () => {
      const userProfile = await userStore.getUser(props.coo.userId);
      user.value = userProfile;
    })
    return {
      user,
      isLiked,
      removeClicked,
      likeClicked,
      relativeDate,
      userStore
    }
  }
}
</script>
  
<style lang="sass" scoped>

</style>