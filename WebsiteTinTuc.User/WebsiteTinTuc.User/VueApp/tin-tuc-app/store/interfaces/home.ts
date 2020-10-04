import { Guid } from "guid-typescript";
import { MoneyType } from "../enums/money-type";
import IObjectFile from "./IObjectFile";

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

export class HomeFilter {
    currentPage: number;
    pageSize: number;
    isHot?: boolean;
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