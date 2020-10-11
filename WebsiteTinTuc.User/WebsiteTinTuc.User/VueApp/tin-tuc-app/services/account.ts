import request from '@/tin-tuc-app/constants/request';

const getUserById = (id: number): Promise<any> => {
    return request({
        method: 'GET',
        url: `/api/services/app/User/Get?Id=${id}`
    });
}

const ACCOUNT_SERVICES = {
    getUserById: getUserById
}

Object.freeze(ACCOUNT_SERVICES);

export default ACCOUNT_SERVICES;