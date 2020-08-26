import IObjectFile from "../interfaces/IObjectFile";

export default class Nationality {
    id: string;
    name: string;
    image?: IObjectFile;
    creationTime: Date;
}