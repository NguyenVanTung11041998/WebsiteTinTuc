import VueRouter from 'vue-router'

// Pages
import Home from '@/tin-tuc-app/views/Home.vue';

const routePrefix = 'home';

const routes = [
	{
		path: '/',
		redirect: { name: 'home' }
	},
	{
		name: 'home',
		path: `/${routePrefix}`,
		component: Home
	},
	{
		name: 'category',
		path: `/${routePrefix}/danh-muc/:id`,
		component: Home
	},
	{
		name: 'hashtag',
		path: `/${routePrefix}/hashtag/:id`,
		component: Home
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
