import { Guid } from "guid-typescript";
import Entity from "./entity";
import IObjectFile from "./IObjectFile";

export default interface Company extends Entity<Guid> {
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
}

export interface HashtagModel extends Entity<Guid> {
    companyId: string;
    hashtagId: string;
    companyName: string;
    hashtagName: string;
}

export interface ProminentCompanyModel extends Entity<Guid> {
    image?: IObjectFile;
    thumbnail?: IObjectFile;
    name: string;
    fullNameCompany: string;
    description: string;
    location: string;
    numberJobs: number;
    companyUrl: string;
}