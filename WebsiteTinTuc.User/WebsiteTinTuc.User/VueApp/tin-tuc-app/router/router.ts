import VueRouter from 'vue-router'

// Pages
import Home from '@/tin-tuc-app/views/Home.vue';
import RoutePath from '../constants/route-path';

const routes = [
	{
		path: '/',
		component: Home
	},
	{
		name: 'home',
		path: `/${RoutePath.Home}`,
		component: Home
	},
	{
		name: 'hashtag',
		path: `/${RoutePath.Hashtag}/:id`,
		component: Home
	},
	{
		name: 'company',
		path: `/${RoutePath.Company}/:id`,
		component: Home
	},
	{
		name: 'post',
		path: `/${RoutePath.Post}/:id`,
		component: Home
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
