import { Guid } from "guid-typescript";
import IObjectFile from "../interfaces/IObjectFile";
import Entity from "./entity";

export default class Nationality extends Entity<Guid> {
    name: string;
    image?: IObjectFile;
    creationTime: Date;
}