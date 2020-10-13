export default interface AccountDto extends AccountInfo {
    fullName: string;
    id: number;
    lastLoginTime: Date;
    creationTime: Date;
}

interface AccountInfo {
    userName: string;
    name: string;
    emailAddress: string;
    isActive: boolean;
    phoneNumber: string;
    roleNames: string[];
}

export interface CreateAccount extends AccountInfo {
    password: string;
}