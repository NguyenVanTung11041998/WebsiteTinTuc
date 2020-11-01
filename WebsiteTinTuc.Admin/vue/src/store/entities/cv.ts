import { Guid } from "guid-typescript";
import Entity from "./entity";

export default class CV extends Entity<Guid> {
    userId?: number;
    link?: string;
    userName?: string;
    name: string;
    email: string;
    phoneNumber: string;
    portfolio?: string;
    isRead: boolean;
    postId: Guid;
    creationTime: Date;
}