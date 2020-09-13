import { Guid } from "guid-typescript";
import Entity from "./entity";

export default interface Hashtag extends Entity<Guid> {
    name: string | null;
    hashtagUrl: string | null;
    creationTime?: Date;
}