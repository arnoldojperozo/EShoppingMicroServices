using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsByNameQuery : IRequest<IList<ProductsResponse>>
{
    public string Name { get; set; }
    
    public GetProductsByNameQuery(string name)
    {
        Name = name;
    }
    
    
}