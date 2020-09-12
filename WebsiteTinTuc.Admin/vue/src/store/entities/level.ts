import { Guid } from "guid-typescript";
import Entity from "./entity";

export default class Level extends Entity<Guid> {
    name: string;
    creationTime: Date;
}