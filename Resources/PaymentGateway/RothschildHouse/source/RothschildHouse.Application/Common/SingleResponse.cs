namespace RothschildHouse.Application.Common;

public record SingleResponse<TModel> : Response
{
    public SingleResponse()
    {
    }

    public SingleResponse(TModel model)
    {
        Model = model;
    }

    public TModel Model { get; set; }
}
