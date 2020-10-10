export default interface PageResultDto<T> {
    totalCount: number;
    items: Array<T>;
}