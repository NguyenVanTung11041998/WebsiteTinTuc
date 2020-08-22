import { FileType } from "../enums/file-type";

export default interface IObjectFile {
    id: string | null;
    file?: File;
    fileType: FileType;
    path: string | null;
}