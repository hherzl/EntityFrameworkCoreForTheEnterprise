export class Response {
    public message!: string;
}

export class ListResponse<TModel> extends Response {
    public model!: TModel[];
}

export class SingleResponse<TModel> extends Response {
    public model!: TModel;
}
