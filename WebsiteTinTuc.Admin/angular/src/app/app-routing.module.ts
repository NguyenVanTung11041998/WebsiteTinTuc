import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { AppRouteGuard } from "@shared/auth/auth-route-guard";
import { HomeComponent } from "./home/home.component";
import { AboutComponent } from "./about/about.component";
import { UsersComponent } from "./users/users.component";
import { TenantsComponent } from "./tenants/tenants.component";
import { HashtagComponent } from "./hashtag/hashtag.component";
import { RolesComponent } from "app/roles/roles.component";
import { ChangePasswordComponent } from "./users/change-password/change-password.component";
import { NationalityComponent } from "./nationality/nationality.component";
import { LevelComponent } from "./level/level.component";
import { BranchJobComponent } from "./branch-job/branch-job.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        children: [
          {
            path: "home",
            component: HomeComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "users",
            component: UsersComponent,
            data: { permission: "Pages.Users" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "roles",
            component: RolesComponent,
            data: { permission: "Pages.Roles" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            component: TenantsComponent,
            data: { permission: "Pages.Tenants" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "hashtag",
            component: HashtagComponent,
            data: { permission: "Pages.View.Hashtag" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "nationality",
            component: NationalityComponent,
            data: { permission: "Pages.View.Nationality" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "level",
            component: LevelComponent,
            data: { permission: "Pages.View.Level" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "branchJob",
            component: BranchJobComponent,
            data: { permission: "Pages.View.BranchJob" },
            canActivate: [AppRouteGuard],
          },
          { path: "about", component: AboutComponent },
          {
            path: "update-password",
            component: ChangePasswordComponent,
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
