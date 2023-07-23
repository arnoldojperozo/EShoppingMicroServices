using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
{
    private readonly IProductTypeRepository _typesRepository;

    public GetAllTypesQueryHandler(IProductTypeRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    
    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _typesRepository.GetAllProductTypes();

        var typesResponseList = ProductMapper.Mapper.Map<IList<TypesResponse>>(typesList);

        return typesResponseList;
    }
}