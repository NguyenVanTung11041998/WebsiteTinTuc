import AccountDto from "../../interfaces/account";
import Authenticate from "../../interfaces/authenticate";

export default interface AccountState {
    userLoginInfo: Authenticate | null;
    loginStatus: boolean;
    user: AccountDto | null;
}