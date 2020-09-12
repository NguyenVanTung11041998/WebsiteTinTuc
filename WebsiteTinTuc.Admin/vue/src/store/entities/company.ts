import IObjectFile from "../interfaces/IObjectFile";
import Post from "./post";
import BranchJobCompany from "./branch-job-company";
import Entity from "./entity";
import { Guid } from "guid-typescript";

export default class Company extends Entity<Guid> {
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
    isHot: boolean;
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

export class HashtagModel extends Entity<Guid> {
    companyId: string;
    hashtagId: string;
    companyName: string;
    hashtagName: string;
}

export class BranchJobCompanyModel extends Entity<Guid> {
    companyId: string;
    branchJobId: string;
    companyName: string;
    branchJobName: string;
}