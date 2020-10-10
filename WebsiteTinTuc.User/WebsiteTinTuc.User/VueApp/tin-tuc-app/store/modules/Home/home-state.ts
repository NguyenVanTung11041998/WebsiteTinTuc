import CompanyPostModel from "../../interfaces/home";

export default interface PostState {
    companyPostProminents: CompanyPostModel[];
    companyPosts: CompanyPostModel[];
    pagePaginate: number;
    pagePaginateButtom: number;
    posts: CompanyPostModel[];
    place: number;
}