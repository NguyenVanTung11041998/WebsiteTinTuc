import CompanyHome, { PostCompanyHome, PostHomeInfoDto } from "../../interfaces/company-home";

export default interface CompanyHomeState {
    company: CompanyHome;
    pagePaginate: number;
    posts: PostCompanyHome[],
    post: PostHomeInfoDto
}