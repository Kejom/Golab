import { api } from "src/boot/axios";
import mgr from "src/oidc/oidc-lite";
import { Notify } from "quasar";

const getAuthHeaders = async () => {
    var user = await mgr.getUser();
    var token = user.access_token;
    return { "Authorization": `Bearer ${token}`, 'Content-type': 'application/json' }
}

const getHeaders = () => { return { 'Content-type': 'application/json' } }

export const getCurrentUserProfile = async () => {
    try {
        var headers = await getAuthHeaders();

        var result = await api.get("/profile", { headers: headers, validateStatus: false })

        if(result.status === 200)
            return result.data;

        return null;

    } catch (error) {
        Notify.create({message: "wystapił błąd podczas pobierania danych z serwera", color: "red"})
    }
}

export const getUserProfile = async(id) => {
    try {
        var result = await api.get(`/profile/${id}`,{headers: getHeaders(), validateStatus: false })

        if(result.status === 200)
            return result.data;

        return null;

    } catch (error) {
        Notify.create({message: "wystapił błąd podczas pobierania danych z serwera", color: "red"})
    }
}