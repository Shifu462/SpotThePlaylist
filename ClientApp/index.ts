import Vue from "vue";
import VueRouter from "vue-router";
import store from './core/store';

Vue.use(VueRouter);
Vue.config.productionTip = false;

import AppComponent from './components/App.vue';
import AuthorizationPanel from "./components/AuthorizationPanel.vue";
import PlaylistTable from './components/PlaylistTable.vue';
import AuthSuccess from './components/AuthSuccess.vue';

const router = new VueRouter({
	mode: 'history',
	routes: [
		{ path: '/', redirect: '/auth' },
		{ path: '/auth', component: AuthorizationPanel },
        { path: '/playlist', component: PlaylistTable }, // сжечь
        { 
            path: '/auth-success',
            component: AuthSuccess,
            props: (route) => {
                const results = /#access_token=(.+)&token_type=(\w+)&/g
                                    .exec(route.hash);
                
                if (results && results.length >= 1) {
                    return { token: results[1] };
                }
            },
        },
	]
});

export default new Vue( {
	el: "#app-root",
    render: h => h(AppComponent),
    router,
    store,
} );