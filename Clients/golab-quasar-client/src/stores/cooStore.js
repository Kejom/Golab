import { defineStore } from "pinia";
import { getCoos } from "src/services/cooService";

export const useCooStore = defineStore("Coo", {
    state: () => ({
        coos: [],
        filter: ''
    }),
    actions: {
        addCoo(coo)    {this.coos.unshift(coo)},
        removeCoo(id) {this.coos = this.coos.filter(c => c.id !== id)},
        editCoo(coo){
            const coos = [...this.coos];
            const index = coos.findIndex(c => c.id === coo.id);
            coos[index] = coo;
            this.coos = coos;
        },
        async initCoos(){
            var coos = await getCoos();
            if(coos)
                this.coos = coos;
        }
    },
    getters: {
        getCoos: (state) => {
            if(state.filter.length < 3)
                return state.coos;
            return state.coos.filter(c => c.content.toLowerCase().includes(state.filter.toLowerCase()));
        },
        getCoosByUserId: (state) => {
            return (userId) => {
                let result = state.coos.filter(c => c.userId === userId);
                if(state.filter.length < 3)
                    return result;
                return result.filter(c => c.content.toLowerCase().includes(state.filter.toLowerCase()));
            }
        },
        getCooById: (state) => {
            return (id) => state.coos.find(c => c.id === id);
        }
    }
})