import request from '@/tin-tuc-app/constants/request';

const createCV = (params: FormData): Promise<any> => {
    return request({
        method: 'POST',
        url: '/api/services/app/Recruitment/CreateCV',
        data: params
    });
}

const RECRUIMENT_SERVICES = {
    createCV: createCV
}

Object.freeze(RECRUIMENT_SERVICES);

export default RECRUIMENT_SERVICES;