import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        token: localStorage.getItem('spotify-token') || '',
        tokenExpires: localStorage.getItem('spotify-token-expires') || '',
    },
    getters: {
        token: state => state.token,
        isLoggedIn: state => {
            if (!state.token) return false;
            
            return true; // TODO: add IsTokenValid call.
        },
    },
    mutations: {
        updateToken(state, token) {
            state.token = token;
            localStorage.setItem('spotify-token', token);
        },
        removeToken(state) {
            state.token = '';
            localStorage.removeItem('spotify-token');
        }
    }
});