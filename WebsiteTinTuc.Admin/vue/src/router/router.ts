declare global {
    interface RouterMeta {
        title: string;
    }
    interface Router {
        path: string;
        name: string;
        icon?: string;
        permission?: string;
        meta?: RouterMeta;
        component: any;
        children?: Array<Router>;
    }
    interface System {
        import(request: string): Promise<any>
    }
    var System: System
}
import main from '@/views/main.vue';
import login from '@/views/login.vue';
import PermissionNames from '@/store/constants/permission-names';
import Path from '@/store/constants/path';
import PathNames from '@/store/constants/path-names';
import roles from '@/views/setting/role/role.vue';
import users from '@/views/setting/user/user.vue';
import tenants from '@/views/setting/tenant/tenant.vue';
import agencies from '@/views/setting/agency/agency.vue';
import createOrEditAgency from '@/views/setting/agency/create-or-edit-agency.vue';
import hashtags from '@/views/setting/hashtag/hashtag.vue';
import posts from '@/views/setting/post/post.vue';

export const locking = {
    path: '/locking',
    name: 'locking',
    component: () => import('../components/lockscreen/components/locking-page.vue')
};
export const loginRouter: Router = {
    path: '/',
    name: 'login',
    meta: {
        title: 'LogIn'
    },
    component: login
};

export const otherRouters: Router = {
    path: '/main',
    name: 'main',
    permission: '',
    meta: { title: 'Menu' },
    component: main,
    children: [
        { path: 'home', meta: { title: 'HomePage' }, name: 'home', component: () => import('../views/home/home.vue') }
    ]
}
export const appRouters: Array<Router> = [{
    path: '/setting',
    name: 'setting',
    permission: '',
    meta: { title: 'Menu' },
    icon: '&#xe68a;',
    component: main,
    children: [
        { path: Path.User, permission: PermissionNames.Pages_Users, meta: { title: 'Người dùng' }, name: PathNames.User, component: users },
        { path: Path.Role, permission: PermissionNames.Pages_Roles, meta: { title: 'Quyền' }, name: PathNames.Role, component: roles },
        { path: Path.Hashtag, permission: PermissionNames.Pages_View_Hashtag, meta: { title: 'Hash tag' }, name: PathNames.Hashtag, component: hashtags },
        { path: Path.Agency, permission: PermissionNames.Pages_View_Agency, meta: { title: 'Công ty' }, name: PathNames.Agency, component: agencies, children: [
            { path: Path.Create, permission: PermissionNames.Pages_Create_Agency, meta: { title: 'Thêm công ty' }, name: PathNames.CreateAgency, component: createOrEditAgency },
            { path: Path.Update, permission: PermissionNames.Pages_Update_Agency, meta: { title: 'Sửa công ty' }, name: PathNames.UpdateAgency, component: createOrEditAgency }
        ]},
        { path: Path.Post, permission: PermissionNames.Pages_View_Post, meta: { title: 'Bài viết' }, name: PathNames.Post, component: posts }
    ]
}]
export const routers = [
    loginRouter,
    locking,
    ...appRouters,
    otherRouters
];
