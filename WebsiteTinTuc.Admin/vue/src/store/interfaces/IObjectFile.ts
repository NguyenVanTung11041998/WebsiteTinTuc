import { FileType } from "../enums/fileType";

export default interface IObjectFile {
    id: string | null;
    file?: File;
    fileType: FileType;
    path: string | null;
}