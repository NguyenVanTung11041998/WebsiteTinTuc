import { Guid } from "guid-typescript";
import { ExperienceType } from "../enums/experience-type";
import { JobType } from "../enums/job-type";
import { MoneyType } from "../enums/money-type";
import Entity from "./entity";
import IObjectFile from "./IObjectFile";
import PageRequest from "./page-request";
import PageResultDto from "./page-result";

export default interface CompanyHome extends Entity<Guid>, CompanyPostHomeInfo {
    images: IObjectFile[];
    description: string;
}

export interface CompanyPostHomeInfo {
    name: string;
    fullNameCompany: string;
    thumbnail?: IObjectFile;
    companyUrl: string;
    website: string;
    locationDescription: string;
    location: string;
    treatment: string;
    minScale?: number;
    maxScale?:number;
    companyJobs: CompanyJobHome[];
    hashtags: HashtagCompanyHome[];
    posts: PageResultDto<PostCompanyHome>;
    nationality: NationalityCompany;
}

export interface PostCompanyHome extends Entity<Guid> {
    title: string;
    location: string;
    minMoney: number;
    maxMoney: number;
    moneyType: MoneyType;
    postUrl: string;
    timeCreateNewJob: number,
    hashtags: HashtagCompanyHome[];
}

export interface CompanyJobHome extends Entity<Guid> {
    name: string;
    companyJobUrl: string;
}

export interface HashtagCompanyHome extends Entity<Guid> {
    name: string;
    hashtagUrl: string;
}

export interface CompanyHomeInfoRequest extends PageRequest {
    url: string;
    postId?: Guid;
}

export interface NationalityCompany {
    name: string;
    path: string;
}

export interface PostHomeInfoDto extends Entity<Guid>, CompanyPostHomeInfo {
    title: string;
    location: string;
    minMoney: number;
    maxMoney: number;
    moneyType: MoneyType;
    postUrl: string;
    hashtagPosts: HashtagCompanyHome[];
    levelName: string;
    endDate?: Date;
    timeExperience?: number;
    experienceType: ExperienceType;
    jobType: JobType;
    timeCreateNewJob: number;
}