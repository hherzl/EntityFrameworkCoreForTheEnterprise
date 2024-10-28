namespace RothschildHouse.Application.Common;

public record ListResponse<TModel> : Response
{
    public ListResponse()
    {
    }

    public ListResponse(IEnumerable<TModel> model)
    {
        Model = new List<TModel>(model);
    }

    public IEnumerable<TModel> Model { get; set; }
}
