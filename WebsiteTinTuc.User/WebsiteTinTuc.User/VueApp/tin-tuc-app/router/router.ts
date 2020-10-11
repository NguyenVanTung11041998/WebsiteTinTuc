import VueRouter from 'vue-router'

// Pages
import Home from '@/tin-tuc-app/views/Home.vue';
import CompanyInfoHome from "@/tin-tuc-app/views/CompanyInfoHome.vue";
import PostInfoHome from "@/tin-tuc-app/views/PostInfoHome.vue";
import ListPosts from '@/tin-tuc-app/views/ListPosts.vue';
import RoutePath from '../constants/route-path';
import RouteName from '../constants/route-name';

const routes = [
	{
		path: '/',
		component: Home
	},
	{
		name: RouteName.Home,
		path: `/${RoutePath.Home}`,
		component: Home
	},
	{
		name: RouteName.ListPostWithoutId,
		path: `/${RoutePath.ListPost}`,
		component: ListPosts
	},
	{
		name: RouteName.ListPost,
		path: `/${RoutePath.ListPost}/:id?`,
		component: ListPosts
	},
	{
		name: RouteName.Hashtag,
		path: `/${RoutePath.Hashtag}/:id`,
		component: Home
	},
	{
		name: RouteName.BranchJob,
		path: `/${RoutePath.BranchJob}/:id`,
		component: Home
	},
	{
		name: RouteName.Company,
		path: `/${RoutePath.Company}/:id`,
		component: CompanyInfoHome
	},
	{
		name: RouteName.Post,
		path: `/${RoutePath.Post}/:id`,
		component: PostInfoHome
	}
]

export const router = new VueRouter({
	mode: 'history',
	routes,
	linkActiveClass: 'is-active'
})
