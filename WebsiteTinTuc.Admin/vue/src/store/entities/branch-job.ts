import BranchJobCompany from "./branch-job-Company";

export default class BranchJob {
    name: string;
    branchJobLocation: string;
    creationTime: Date;
    branchJobCompanies?: Array<BranchJobCompany>;
}