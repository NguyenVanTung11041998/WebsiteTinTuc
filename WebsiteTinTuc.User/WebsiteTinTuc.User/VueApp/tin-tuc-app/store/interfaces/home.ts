import { Guid } from "guid-typescript";
import { MoneyType } from "../enums/money-type";
import IObjectFile from "./IObjectFile";

export default interface CompanyPostModel {
    companyId: Guid;
    postId: Guid;
    fullCompanyName: string;
    thumbnail?: IObjectFile;
    location: string;
    timeCreateNewJob: number;
    minSalary: number;
    maxSalary: number;
    moneyType: MoneyType;
    title: string;
    treatment: string;
    postUrl: string;
}

export class HomeFilter {
    currentPage: number;
    pageSize: number;
    isHot?: boolean;
}