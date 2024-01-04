import { api } from "src/boot/axios";
import mgr from "src/oidc/oidc-lite";
import { Notify } from "quasar";

const getAuthHeaders = async () => {
    var user = await mgr.getUser();
    var token = user.access_token;
    return { "Authorization": `Bearer ${token}`, 'Content-type': 'application/json' }
}

const getHeaders = () => { return { 'Content-type': 'application/json' } }


export const getCoos = async () => {
    var result = await api.get("/coos", { headers: getHeaders() })

    if (result.status === 200)
        return result.data;

    Notify.create("Wystapił problem podczas pobierania danych z serwera ")
    return [];
}

export const getCooById = async (id) => {
    var result = await api.get(`/coos/${id}`, {headers: getHeaders()})

    if(result.status === 200)
        return result.data;

        Notify.create("Wystapił problem podczas pobierania danych z serwera ")
        return null;      
}


export const postCoo = async (coo) => {
    try {
        var headers = await getAuthHeaders();
        var result = await api.post("/coos", coo, { headers: headers });

        if (result.status !== 200)
            throw error;
        
        Notify.create({ message: "Dodano nowe gruchnięcie", color: "green" });
        return result.data;
    } catch (error) {
        Notify.create({ message: "Wystąpił problem podczas dodawania gruchnięcia", color: "red" })
    }
}

export const deleteCoo = async (id) => {
    try {
        var headers = await getAuthHeaders();
        var result = await api.delete(`/coos/${id}`, { headers: headers });

        if (result.status !== 200)
            throw error;
        return true;
    } catch (error) {
        Notify.create({ message: "Wystąpił problem podczas dodawania gruchnięcia", color: "red" })
        return false;
    }
}

