import VueRouter from 'vue-router'

// Pages
import Home from '@/tin-tuc-app/views/Home.vue';

const routePrefix = 'trang-chu';

const routes = [
	{
		path: '/',
		component: Home
	},
	{
		name: 'home',
		path: `/${routePrefix}`,
		component: Home
	},
	{
		name: 'hashtag',
		path: `/${routePrefix}/hashtag/:id`,
		component: Home
	},
	{
		name: 'company',
		path: `/${routePrefix}/cong-ty/:id`,
		component: Home
	},
	{
		name: 'post',
		path: `/${routePrefix}/viec-lam/:id`,
		component: Home
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
