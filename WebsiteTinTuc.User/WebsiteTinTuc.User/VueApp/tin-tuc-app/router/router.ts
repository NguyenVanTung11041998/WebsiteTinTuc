import VueRouter from 'vue-router'

// Pages
import Vuex from '@/tin-tuc-app/views/Vuex.vue';
import TemplateInfo from '@/tin-tuc-app/views/TemplateInfo.vue';
import ThirdPartyLibraries from '@/tin-tuc-app/views/ThirdPartyLibraries.vue';
import MenuCategoryHashtag from '@/tin-tuc-app/views/MenuCategoryHashtag.vue';

const routePrefix = 'home';

const routes = [
	{
		path: '*',
		redirect: { name: 'home' }
	},
	{
		name: 'home',
		path: `/${routePrefix}/danh-muc/:id`,
		component: TemplateInfo
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
