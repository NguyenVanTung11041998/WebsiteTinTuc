import IObjectFile from "../interfaces/IObjectFile";
import Post from "./post";
import BranchJobCompany from "./branch-job-company";

export default class Company {
    id?: string;
    name: string;
    creationTime: Date;
    description: string;
    phone: string;
    email: string;
    locationDescription: string;
    location: string;
    fullNameCompany: string;
    website: string;
    minScale?: number;
    maxScale?: number;
    treatment: string;
    nationalityId: string;
    nationalityCompanyName: string;
    userId: number;
    creatorName: string;
    thumbnail?: IObjectFile;
    nationalityImage?: IObjectFile;
    images?: Array<IObjectFile>;
    hashtags?: Array<HashtagModel>;
    posts?: Array<Post>;
    branchJobCompanies?: Array<BranchJobCompanyModel>;
}

export class HashtagModel {
    id: string;
    companyId: string;
    hashtagId: string;
    companyName: string;
    hashtagName: string;
}

export class BranchJobCompanyModel {
    id: string;
    companyId: string;
    branchJobId: string;
    companyName: string;
    branchJobName: string;
}