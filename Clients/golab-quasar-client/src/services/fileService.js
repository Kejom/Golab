import { api } from "src/boot/axios";
import mgr from "src/oidc/oidc-lite";
import { Notify } from "quasar";

const getAuthHeaders = async () => {
    var user = await mgr.getUser();
    var token = user.access_token;
    return { "Authorization": `Bearer ${token}`, 'Content-type': 'multipart/form-data' }
}


export const postAvatarFile = async(file) => {
    try {
      var headers = await getAuthHeaders();
      const formData = new FormData();
      formData.append('file', file);
      
      var result = await api.post("/files/avatar", formData, {headers: headers})

      if(result.status !== 200)
        throw error;
    
    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas zapisywania avatara", color: "red" })
    }
}