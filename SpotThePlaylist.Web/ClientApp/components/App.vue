<template>

	<div class="page-content">
		
        <transition name="router-anim"
                    mode="out-in"
                    enter-active-class="animated fadeInLeft" 
                    leave-active-class="animated fadeOutRight">
		    <router-view></router-view>
        </transition>
	</div>

</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";

@Component({})
export default class AppComponent extends Vue {

}

</script>


<style lang="less">

// -- animations --
@import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.7.2/animate.min.css';
.fadeInLeft {
    -webkit-animation-name: fadeInLeft;
    animation-name: fadeInLeft;
    -webkit-animation-duration: 250ms;
    animation-duration: 250ms;
}

.fadeOutRight {
    -webkit-animation-name: fadeOutRight;
    animation-name: fadeOutRight;
    -webkit-animation-duration: 250ms;
    animation-duration: 250ms;
}

// ---

.page-content {
	width: 100%;
	height: 100%;

    font-family: 'Montserrat', sans-serif;
    
    display: flex;
	align-items: center;
	justify-content: center;
	flex-direction: column;
}

</style>