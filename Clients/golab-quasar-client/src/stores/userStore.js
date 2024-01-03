import { defineStore } from "pinia";
import { getUserProfile } from "src/services/profileService";

export const useUserStore = defineStore('userData', {
    state: () => ({
        loggedUserId: null,
        loggedUserProfile: null,
        userCache: {},
        loggedUserLikes: new Set()
    }),
    actions: {
        setLoggeduserId(id){this.loggedUserId = id},
        setLoggedUserProfile(profile){this.loggedUserProfile = profile},
        setLoggedUserLikes(likes) {this.loggedUserLikes = new Set(likes)},
        async getUser(id){
            if(Object.hasOwn(this.userCache, id))
                return this.userCache[id];

            const profile = await getUserProfile(id);
            
            if(profile)
                this.userCache[id] = profile;
            return profile;
        },
        updateUserCache(userProfile) {this.userCache[userProfile.id] = userProfile},
        addLike(cooId) {this.loggedUserLikes.add(cooId)},
        removeLike(cooId){this.loggedUserLikes.delete(cooId)}
    }
}
)