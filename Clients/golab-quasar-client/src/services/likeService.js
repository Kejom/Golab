import { api } from "src/boot/axios";
import mgr from "src/oidc/oidc-lite";
import { Notify } from "quasar";

const getAuthHeaders = async () => {
    var user = await mgr.getUser();
    var token = user.access_token;
    return { "Authorization": `Bearer ${token}`, 'Content-type': 'application/json' }
}

export const getLikes = async() => {
try {
    var headers = await getAuthHeaders();

    var result = await api.get("/likes", {headers: headers});

    if(result.status !== 200)
        throw error;

    var likes = new Set();
    result.data.forEach(like => {
        likes.add(like.cooId)
    })

    return likes;

} catch (error) {
    Notify.create("Wystapił problem podczas pobierania danych z serwera ")
    return new Set();
}
}

export const likeCoo = async(id) => {
    try {
        var headers = await getAuthHeaders();

        var result = await api.post("/likes", {ValueChange: 1, CooId: id}, {headers: headers})

        if(result.status !== 200)
            throw error;

        return true;

    } catch (error) {
        Notify.create("Wystapił problem podczas kontaktu z serwerem")
        return false;
    }
}

export const unlikeCoo = async(id) => {
    try {
        var headers = await getAuthHeaders();

        var result = await api.post("/likes", {ValueChange: -1, CooId: id}, {headers: headers})

        if(result.status !== 200)
            throw error;

        return true;
     
    } catch (error) {
        Notify.create("Wystapił problem podczas kontaktu z serwerem")
        return false;
    }
}