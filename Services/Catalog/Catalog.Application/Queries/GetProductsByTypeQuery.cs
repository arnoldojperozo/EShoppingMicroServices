
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsByTypeQuery : IRequest<IList<ProductResponse>>
{
    public string TypeName { get; set; }

    public GetProductsByTypeQuery(string typeName)
    {
        TypeName = typeName;
    }
}