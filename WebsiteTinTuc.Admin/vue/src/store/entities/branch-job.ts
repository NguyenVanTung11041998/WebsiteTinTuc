import { Guid } from "guid-typescript";
import BranchJobCompany from "./branch-job-Company";
import Entity from "./entity";

export default class BranchJob extends Entity<Guid> {
    name: string;
    branchJobLocation: string;
    creationTime: Date;
    branchJobCompanies?: Array<BranchJobCompany>;
}