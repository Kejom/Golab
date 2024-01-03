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

        var result = await api.get("/profiles", { headers: headers, validateStatus: false })

        if (result.status === 200)
            return result.data;

        return null;

    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas pobierania danych z serwera", color: "red" })
    }
}

export const getUserProfile = async (id) => {
    try {
        var result = await api.get(`/profiles/${id}`, { headers: getHeaders(), validateStatus: false })

        if (result.status === 200)
            return result.data;

        return null;

    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas pobierania danych z serwera", color: "red" })
    }
}

export const getUserProfileByHandle = async (handle) => {
    try {
        var result = await api.get(`/profiles/handle/${handle}`, {headers: getHeaders()})

        if (result.status !== 200)
            throw error;

        return result.data;
    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas pobierania danych z serwera", color: "red" });
    }
}

export const postUserProfile = async (userProfile) => {
    try {
        var headers = await getAuthHeaders();
        var result = await api.post('/profiles', userProfile, { headers: headers })

        if (result.status === 200)
            return result.data;

        return null;
    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas zapisywania profilu", color: "red" })
    }
}

export const putUserProfile = async (userProfile) => {
    try {
        var headers = await getAuthHeaders();
        var result = await api.put('/profiles', userProfile, { headers: headers })

        if (result.status === 200)
            return result.data;

        return null;
    } catch (error) {
        Notify.create({ message: "wystapił błąd podczas zapisywania zmian w profilu", color: "red" })
    }
}