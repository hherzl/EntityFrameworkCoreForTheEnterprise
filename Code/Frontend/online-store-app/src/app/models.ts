export class PagedResponse<TModel> {
    public message: string;
    public didError: boolean;
    public errorMessage: string;
    public model: TModel[];
    public pageSize: number;
    public pageNumber: number;
    public itemsCount: number;
    public pageCount: number;
}

export class OrderInfo {
    public orderID: number;
    public orderStatusID: number;
    public orderStatusDescription: string;
    public customerCompanyName: string;
    public total: number;
}
