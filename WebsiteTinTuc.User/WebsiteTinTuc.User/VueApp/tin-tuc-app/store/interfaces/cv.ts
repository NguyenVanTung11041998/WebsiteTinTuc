import { Guid } from "guid-typescript";
import Entity from "./entity";

export default interface CV extends Entity<Guid> {
    userId?: number;
    name?: string;
    link?: string;
    userName?: string;
    email?: string;
    portfolio?: string;
    postId: Guid;
    phoneNumber?: string;
}