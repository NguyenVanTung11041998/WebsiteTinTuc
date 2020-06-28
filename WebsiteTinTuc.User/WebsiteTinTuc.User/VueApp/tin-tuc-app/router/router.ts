import VueRouter from 'vue-router'

// Pages
import Vuex from '@/tin-tuc-app/views/Vuex.vue'
import TemplateInfo from '@/tin-tuc-app/views/TemplateInfo.vue'
import ThirdPartyLibraries from '@/tin-tuc-app/views/ThirdPartyLibraries.vue'

const routePrefix = 'template'

const routes = [
	{
		path: '*',
		redirect: { name: 'templateInfo' }
	},
	{
		name: 'templateInfo',
		path: `/${routePrefix}/info`,
		component: TemplateInfo
	},
	{
		name: 'vuex',
		path: `/${routePrefix}/vuex`,
		component: Vuex
	},
	{
		name: 'thirdpartylibraries',
		path: `/${routePrefix}/thirdpartylibraries`,
		component: ThirdPartyLibraries
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
