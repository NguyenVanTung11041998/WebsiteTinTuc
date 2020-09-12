import { Guid } from "guid-typescript";
import Entity from "./entity";

export interface Hashtag extends Entity<Guid> {
    name: string | null;
    hashtagUrl: string | null;
    creationTime?: Date;
}