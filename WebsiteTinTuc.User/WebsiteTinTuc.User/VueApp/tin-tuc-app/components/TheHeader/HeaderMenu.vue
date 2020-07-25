<template>
    <div class="header__topbar">
        <div class="container">
            <b-navbar>
                <b-navbar-nav>
                    <b-nav-item @click="redirect">
                        <i class="fas fa-headset"></i> CSKH
                    </b-nav-item>
                    <b-nav-item v-if="!isAuthenticated">
                        <router-link to="/home/register">
                            <i class="fas fa-user-circle"></i> Đăng Ký
                        </router-link>
                    </b-nav-item>
                    <b-nav-item v-if="isAuthenticated">
                        <router-link to="/home/edit-account">
                            <i class="fas fa-user-circle"></i> Sửa tài khoản
                        </router-link>
                    </b-nav-item>
                    <b-nav-item v-if="!isAuthenticated">
                        <router-link to="/home/sign-in">
                            <i class="fas fa-user-circle"></i> Đăng Nhập
                        </router-link>
                    </b-nav-item>
                    <b-nav-item-dropdown v-if="isAuthenticated" right>
                        <template slot="text">
                            <i class="fas fa-user-circle"></i>
                            Username
                        </template>
                        <b-dropdown-item @click="logout" style="text-align:center;">Logout</b-dropdown-item>
                    </b-nav-item-dropdown>
                </b-navbar-nav>
            </b-navbar>
        </div>
    </div>
</template>


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { Getter, Action } from 'vuex-class'
//import { UserDetails } from '../../types/userModel'

@Component({
    name: 'HeaderMenu'
})
export default class HeaderMenu extends Vue {
    // @Getter('userDetails', { namespace: 'AppModule' })
    // public userDetails!: UserDetails
    // @Getter('isAuthenticated', { namespace: 'AppModule' })
    // public isAuthenticated!: boolean

    // @Action('logout', { namespace: 'AppModule' })
    // public actionLogout!: () => void

    public isAuthenticated = false;
    public logout() {
        //this.actionLogout();
        this.$router.push('/home/sign-in');
    }

    private redirect() {
        window.open("https://www.facebook.com/C%E1%BB%95ng-Game-H5-Metap-105893557565069/", "_blank");
    }
}

</script>

<style lang="scss" scoped>
    .header__topbar {
        width: 100%;
        background-color: #CCCCCC;
        .navbar {
            background-color: #CCCCCC;
        }
    }
</style>
