export default class GrantedPermission {
    static grantedPermissions: string[] = [];
    static isGranted(permission: string): boolean {
        let index = this.grantedPermissions.findIndex(x => x === permission);
        return index > -1;
    }
}