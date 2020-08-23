import BranchJobCompany from "./branch-job-Company";

export default class BranchJob {
    name: string;
    branchJobLocation: string;
    branchJobCompanies?: Array<BranchJobCompany>;
}