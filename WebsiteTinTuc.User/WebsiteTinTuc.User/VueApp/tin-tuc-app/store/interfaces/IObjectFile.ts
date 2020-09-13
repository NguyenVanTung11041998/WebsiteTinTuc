import { Guid } from "guid-typescript";
import { FileType } from "../enums/file-type";
import Entity from "./entity";

export default interface IObjectFile extends Entity<Guid> {
    file?: File;
    fileType: FileType;
    path: string;
}