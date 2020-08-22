import IObjectFile from "../interfaces/IObjectFile";
import Hashtag from "./hashtag";
import Post from "./post";
import BranchJobAgency from "./branch-job-agency";

export default class Agency {
    name: string;
    description: string;
    phone: string;
    email: string;
    locationDescription: string;
    location: string;
    descrtionAgency: string;
    website: string;
    minScale?: number;
    maxScale?: number;
    treatment: string;
    nationalityAgency: string;
    userId: number;
    creatorName: string;
    thumbnail?: IObjectFile;
    nationalityImage?: IObjectFile;
    images?: Array<IObjectFile>;
    hashtags?: Array<Hashtag>;
    posts?: Array<Post>;
    branchJobAgencies?: Array<BranchJobAgency>;
}