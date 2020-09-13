import { Guid } from "guid-typescript";
import Entity from "./entity";

export default interface Post extends Entity<Guid>, PostTopProminent {
    content: string;
}

export interface PostTopProminent extends Entity<Guid> {
    companyName: string;
    title: string;
    postUrl: string;
}