import request from '@/tin-tuc-app/constants/request';
import AccountDto, { CreateAccount } from '../store/interfaces/account';

const getUserById = (id: number): Promise<any> => {
    return request({
        method: 'GET',
        url: `/api/services/app/User/Get?Id=${id}`
    });
}

const createUser = (params: CreateAccount): Promise<any> => {
    return request({
        method: 'POST',
        url: `/api/services/app/User/CreateUser`,
        data: params
    });
}

const updateUser = (params: AccountDto): Promise<any> => {
    return request({
        method: 'PUT',
        url: `/api/services/app/User/UpdateCurrentUser`,
        data: params
    });
}

const ACCOUNT_SERVICES = {
    getUserById: getUserById,
    createUser: createUser,
    updateUser: updateUser
}

Object.freeze(ACCOUNT_SERVICES);

export default ACCOUNT_SERVICES;