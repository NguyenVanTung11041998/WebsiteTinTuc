import { Guid } from "guid-typescript";
import Entity from "./entity";

export default class BranchJobCompany extends Entity<Guid> {
    companyId: string;
    companyName: string;
    branchJobId: string;
    branchJobName: string;
}