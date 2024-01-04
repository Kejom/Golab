import { api } from "src/boot/axios";
import mgr from "src/oidc/oidc-lite";
import { Notify } from "quasar";

const getAuthHeaders = async () => {
    var user = await mgr.getUser();
    var token = user.access_token;
    return { "Authorization": `Bearer ${token}`, 'Content-type': 'application/json' }
}

const getHeaders = () => { return { 'Content-type': 'application/json' } }


export const getComments = async (cooId) => {
    var result = await api.get(`/comments/${cooId}`, { headers: getHeaders() })

    if (result.status === 200)
        return result.data;

    Notify.create("Wystapił problem podczas pobierania danych z serwera ")
    return [];
}

export const postComment = async (comment) => {
    try {
        var headers = await getAuthHeaders();

        var result = await api.post("/comments", comment, {headers: headers})

        if(result.status !== 200)
            throw error;

        return result.data;

    } catch (error) {
        Notify.create({ message: "Wystąpił problem podczas dodawania komentarza", color: "red" })
    }
}

export const putComment = async (comment) => {
    try {
        var headers = await getAuthHeaders();

        var result = await api.put("/comments", comment, {headers: headers})

        if(result.status !== 200)
            throw error;

        return result.data;

    } catch (error) {
        Notify.create({ message: "Wystąpił problem podczas edycji komentarza", color: "red" })
    }
}

export const deleteComment = async(id) => {
    try {
        var headers = await getAuthHeaders();
        var result = await api.delete(`/comments/${id}`, { headers: headers });

        if (result.status !== 200)
            throw error;
        return true;
    } catch (error) {
        Notify.create({ message: "Wystąpił problem podczas dodawania gruchnięcia", color: "red" })
        return false;
    }
}