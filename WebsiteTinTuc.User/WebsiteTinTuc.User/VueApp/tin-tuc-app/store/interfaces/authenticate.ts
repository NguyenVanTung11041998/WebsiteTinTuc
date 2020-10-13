export default interface Authenticate {
    accessToken: string;
    encryptedAccessToken: string;
    expireInSeconds: number;
    userId: number;
}

export interface AuthenticateRequest {
    userNameOrEmailAddress: string;
    password: string;
    rememberClient: boolean;
}

export interface AppUserLoginInfomation {
    applicaton: Application;
    user: User | null;
    tenant: Tenant | null;
}

interface Application {
    version: string;
    releaseDate: Date;
    features: object;
}

export interface User {
    name: string;
    surname: string;
    userName: string;
    emailAddress: string;
    id: number;
}

interface Tenant {
    tenancyName: string;
    name: string;
    id: number;
}