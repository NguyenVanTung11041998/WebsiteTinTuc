declare global {
    interface RouterMeta {
        title: string;
    }
    interface Router {
        path: string;
        hidden?: boolean;
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
import companies from '@/views/setting/company/company.vue';
import createOrEditCompany from '@/views/setting/company/create-or-edit-company.vue';
import hashtags from '@/views/setting/hashtag/hashtag.vue';
import posts from '@/views/setting/post/post.vue';
import updatePost from '@/views/setting/post/update-post.vue';
import nationalities from '@/views/setting/nationality/nationality.vue';
import branchJobs from '@/views/setting/branchJob/branch-job.vue';
import levels from '@/views/setting/level/level.vue';
import config from '@/views/setting/config/config.vue';
import recruitments from "@/views/setting/recruitment/recruitment.vue";

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
        { path: Path.Hashtag, permission: PermissionNames.Pages_View_Hashtag, meta: { title: 'Hashtag' }, name: PathNames.Hashtag, component: hashtags },
        { path: Path.Company, permission: PermissionNames.Pages_View_Company, meta: { title: 'Công ty' }, name: PathNames.Company, component: companies },
        { path: `${Path.Company}/${Path.Create}`, hidden: true, permission: PermissionNames.Pages_Create_Company, meta: { title: 'Thêm công ty' }, name: PathNames.CreateCompany, component: createOrEditCompany },
        { path: `${Path.Company}/${Path.Update}/:id`, hidden: true, permission: PermissionNames.Pages_Update_Company, meta: { title: 'Sửa công ty' }, name: PathNames.UpdateCompany, component: createOrEditCompany },
        { path: Path.Post, permission: PermissionNames.Pages_View_Post, meta: { title: 'Bài viết' }, name: PathNames.Post, component: posts },
        { path: `${Path.Post}/${Path.Create}`, hidden: true, permission: PermissionNames.Pages_Create_Post, meta: { title: 'Thêm bài viết' }, name: PathNames.CreatePost, component: updatePost },
        { path: `${Path.Post}/${Path.Update}/:id`, hidden: true, permission: PermissionNames.Pages_Update_Post, meta: { title: 'Sửa bài viết' }, name: PathNames.UpdatePost, component: updatePost },
        { path: Path.Nationality, permission: PermissionNames.Pages_View_Nationality, meta: { title: 'Quốc tịch' }, name: PathNames.Nationality, component: nationalities },
        { path: Path.BranchJob, permission: PermissionNames.Pages_View_BranchJob, meta: { title: 'Ngành nghề' }, name: PathNames.BranchJob, component: branchJobs },
        { path: Path.Level, permission: PermissionNames.Pages_View_Level, meta: { title: 'Cấp độ' }, name: PathNames.Level, component: levels },
        { path: Path.Config, permission: PermissionNames.Pages_Change_Config, meta: { title: 'Cấu hình' }, name: PathNames.Config, component: config },
        { path: `${Path.Recruitment}/:id`, permission: PermissionNames.Pages_View_CV, hidden: true, meta: { title: 'Cấu hình' }, name: PathNames.Recruitment, component: recruitments }
    ]
}]
export const routers = [
    loginRouter,
    locking,
    ...appRouters,
    otherRouters
];
