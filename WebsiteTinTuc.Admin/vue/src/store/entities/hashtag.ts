import { Guid } from "guid-typescript";
import Entity from "./entity";

export default class Hashtag extends Entity<Guid> {
    name: string;
    hashtagUrl: string;
    isHot: boolean;
}