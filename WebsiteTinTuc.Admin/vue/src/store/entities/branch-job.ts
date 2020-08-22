import BranchJobAgency from "./branch-job-agency";

export default class BranchJob {
    name: string;
    branchJobLocation: string;
    branchJobAgencies?: Array<BranchJobAgency>;
}