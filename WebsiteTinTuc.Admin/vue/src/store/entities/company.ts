import IObjectFile from "../interfaces/IObjectFile";
import Hashtag from "./hashtag";
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
    nationalityCompanyId: string;
    nationalityCompanyName: string;
    userId: number;
    creatorName: string;
    thumbnail?: IObjectFile;
    nationalityImage?: IObjectFile;
    images?: Array<IObjectFile>;
    hashtags?: Array<Hashtag>;
    posts?: Array<Post>;
    branchJobCompanies?: Array<BranchJobCompany>;
}