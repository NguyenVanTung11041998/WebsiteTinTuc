import { Guid } from "guid-typescript";
import { MoneyType } from "../enums/money-type";
import IObjectFile from "./IObjectFile";
import PageRequest from "./page-request";

export default interface CompanyPostModel {
    companyId: Guid;
    postId: Guid;
    fullNameCompany: string;
    companyName: string;
    thumbnail?: IObjectFile;
    location: string;
    timeCreateNewJob: number;
    minSalary: number;
    maxSalary: number;
    moneyType: MoneyType;
    title: string;
    treatment: string;
    postUrl: string;
    companyJobs: BranchJobCompanyHome[];
    postHashtags: HashtagHomeModel[];
}

export class HomeFilter extends PageRequest {
    isHot?: boolean;
}

export class PostFilter extends PageRequest {
    location: string;
}

export class BranchJobCompanyHome {
    branchJobId: Guid;
    name: string;
    branchJobUrl: string;
}

export class HashtagHomeModel {
    hashtagId: Guid;
    name: string;
    hashtagUrl: string;
}