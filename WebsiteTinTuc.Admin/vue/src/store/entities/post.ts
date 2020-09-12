import { JobType } from "../enums/job-type";
import { MoneyType } from "../enums/money-type";
import { ExperienceType } from "../enums/experience-type";
import { HashtagModel } from "./company";
import Entity from "./entity";
import { Guid } from "guid-typescript";

export default class Post extends Entity<Guid> {
    title: string;
    content: string;
    creationTime: Date;
    postUrl: string;
    numberOfViews: number;
    jobType: JobType;
    minMoney: number;
    maxMoney: number;
    moneyType: MoneyType;
    timeExperience?: number;
    endDate?: Date;
    experienceType: ExperienceType;
    companyId: string;
    levelId: string;
    companyName: string;
    levelName: string;
    hashtags: HashtagModel[];
}

export class CategoryHashtagModel extends Entity<Guid> {
    categoryHashtagOfPostId: string;
}