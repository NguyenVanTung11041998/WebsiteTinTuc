import IObjectFile from "../interfaces/IObjectFile";

export default class Post {
    id: string;
    title: string;
    content: string;
    description: string;
    postUrl: string;
    numberOfViews: number;
    numberOfLikes: number;
    numberOfComments: number;
    objectFile?: IObjectFile;
}