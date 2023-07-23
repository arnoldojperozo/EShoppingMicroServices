using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<ProductTypeResponse>>
{
    private readonly IProductTypeRepository _typesRepository;

    public GetAllTypesQueryHandler(IProductTypeRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    
    public async Task<IList<ProductTypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _typesRepository.GetAllProductTypes();

        var typesResponseList = ProductMapper.Mapper.Map<IList<ProductTypeResponse>>(typesList);

        return typesResponseList;
    }
}