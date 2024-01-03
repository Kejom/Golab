<template>
    <q-item class="qweet q-py-md">
      <q-item-section avatar top>
        <q-btn v-if="user" flat :to="`/${user.handle}`">
        <q-avatar v-if="user" size="xl">
          <img  :src="user.avatarUrl">
        </q-avatar>
        <q-avatar v-if="!user" size="xl">
            <img src="../../assets/golab-default-avatar.jpg">
        </q-avatar>
      </q-btn>
      </q-item-section>
      <q-item-section>
        <q-item-label class="text-subtitle1">
          <strong>{{ user ? user.name : "nieznany" }}</strong>
          <span class="text-grey-7"> @{{ user ? user.handle: "nieznany" }}
            <br class="lt-md">&bull; {{ relativeDate(coo.created) }} temu
          </span>
        </q-item-label>
  
          <q-item-label class="qweet-content text-body1">
          {{ coo.content }}
        </q-item-label>
  
  
        <div class="qweet-icons row justify-around q-mt-md">
          <q-btn flat round color="grey" icon="far fa-comment" size="sm" :to="`/${coo.id}`" :label="coo.comments" text-color="primary" />
          <q-btn flat round color="grey" icon="fas fa-retweet" size="sm" text-color="primary"/>
          <q-btn flat round :color="isLiked ? 'pink' : 'grey'" :icon="isLiked? 'fas fa-heart' : 'far fa-heart'"
            size="sm" @click="likeClicked" :label="coo.likes" text-color="primary" :disable="!userStore.loggedUserId"/>
          <q-btn flat round color="grey" :icon="userStore.loggedUserId === coo.userId ? 'fas fa-trash': ''" size="sm" @click="removeClicked" :disable="userStore.loggedUserId !== coo.userId" text-color="primary"/>
        </div>
      </q-item-section>
  
    </q-item>
  </template>
  
  <script>
  import { ref, onMounted, computed } from 'vue'
  import { formatDistance } from 'date-fns'
  import { pl } from 'date-fns/locale'
  import { useUserStore } from 'src/stores/userStore'
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
      const user = ref({})
      const isLiked = computed(() => userStore.loggedUserLikes.has(props.coo.id))
      const removeClicked = () => console.log("remove clicked");
  
      const likeClicked = () => console.log("like clikced");
  
      const relativeDate = (value) => formatDistance(value, Date.now(), { locale: pl })
  
      onMounted(async() => {
        const userProfile = await userStore.getUser(props.coo.userId);
        console.log(userProfile);
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
  
  <style lang="sass" scoped></style>