import Vue from "vue";
import VueRouter from "vue-router";
import { library, IconDefinition } from '@fortawesome/fontawesome-svg-core'
import { faSync, faSave } from '@fortawesome/free-solid-svg-icons'
import { faSpotify } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import store from './core/store';

library.add(faSync, faSave, faSpotify as IconDefinition);
Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.use(VueRouter);
Vue.config.productionTip = false;

import AppComponent from './components/App.vue';
import AuthIndex from './components/Auth/AuthIndex.vue';
import AuthSuccess from './components/AuthSuccess.vue';
import MainIndex from './components/Main/MainIndex.vue';

const router = new VueRouter({
    mode: 'history',
    routes: [
        { path: '/', component: MainIndex },
        { path: '/auth', component: AuthIndex },
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