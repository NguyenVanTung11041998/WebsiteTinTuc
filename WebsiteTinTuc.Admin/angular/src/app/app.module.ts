import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientJsonpModule, HttpClientModule } from "@angular/common/http";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { TabsModule } from "ngx-bootstrap/tabs";
import { NgxPaginationModule } from "ngx-pagination";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ServiceProxyModule } from "@shared/service-proxies/service-proxy.module";
import { SharedModule } from "@shared/shared.module";
import { HomeComponent } from "@app/home/home.component";
import { AboutComponent } from "@app/about/about.component";
// tenants
import { TenantsComponent } from "@app/tenants/tenants.component";
import { CreateTenantDialogComponent } from "./tenants/create-tenant/create-tenant-dialog.component";
import { EditTenantDialogComponent } from "./tenants/edit-tenant/edit-tenant-dialog.component";
// hashtag
import { EditHashtagDialogComponent } from "./hashtag/edit-hashtag/edit-hashtag-dialog.component";
import { CreateHashtagDialogComponent } from "./hashtag/create-hashtag/create-hashtag-dialog.component";
// roles
import { RolesComponent } from "@app/roles/roles.component";
import { CreateRoleDialogComponent } from "./roles/create-role/create-role-dialog.component";
import { EditRoleDialogComponent } from "./roles/edit-role/edit-role-dialog.component";
// users
import { UsersComponent } from "@app/users/users.component";
import { CreateUserDialogComponent } from "@app/users/create-user/create-user-dialog.component";
import { EditUserDialogComponent } from "@app/users/edit-user/edit-user-dialog.component";
import { ChangePasswordComponent } from "./users/change-password/change-password.component";
import { ResetPasswordDialogComponent } from "./users/reset-password/reset-password.component";
// layout
import { HeaderComponent } from "./layout/header.component";
import { HeaderLeftNavbarComponent } from "./layout/header-left-navbar.component";
import { HeaderLanguageMenuComponent } from "./layout/header-language-menu.component";
import { HeaderUserMenuComponent } from "./layout/header-user-menu.component";
import { FooterComponent } from "./layout/footer.component";
import { SidebarComponent } from "./layout/sidebar.component";
import { SidebarLogoComponent } from "./layout/sidebar-logo.component";
import { SidebarUserPanelComponent } from "./layout/sidebar-user-panel.component";
import { SidebarMenuComponent } from "./layout/sidebar-menu.component";
import { HashtagComponent } from "./hashtag/hashtag.component";
import { NationalityComponent } from "./nationality/nationality.component";
import { CreatNationalityComponent } from "./nationality/creat-nationality/creat-nationality.component";
import { EditNationalityComponent } from "./nationality/edit-nationality/edit-nationality.component";
import { LevelComponent } from "./level/level.component";
import { CreateLevelComponent } from "./level/create-level/create-level.component";
import { EditLevelComponent } from "./level/edit-level/edit-level.component";
import { BranchJobComponent } from "./branch-job/branch-job.component";
import { CreateBranchJobComponent } from "./branch-job/create-branch-job/create-branch-job.component";
import { EditBranchJobComponent } from "./branch-job/edit-branch-job/edit-branch-job.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // hashtag
    HashtagComponent,
    EditHashtagDialogComponent,
    CreateHashtagDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,

    // layout
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLanguageMenuComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    SidebarComponent,
    SidebarLogoComponent,
    SidebarUserPanelComponent,
    SidebarMenuComponent,
    // Nationality
    NationalityComponent,
    CreatNationalityComponent,
    EditNationalityComponent,
    // Level
    LevelComponent,
    CreateLevelComponent,
    EditLevelComponent,
    BranchJobComponent,
    CreateBranchJobComponent,
    EditBranchJobComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
  ],
  providers: [],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // hashtag
    EditHashtagDialogComponent,
    CreateHashtagDialogComponent,
    // Nationality
    CreatNationalityComponent,
    EditNationalityComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
    // Level
    CreateLevelComponent,
    EditLevelComponent,
    // BranchJob
    CreateBranchJobComponent,
    EditBranchJobComponent,
  ],
})
export class AppModule {}
