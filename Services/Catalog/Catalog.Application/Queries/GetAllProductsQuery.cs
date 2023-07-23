﻿using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductsQuery : IRequest<IList<ProductsResponse>>
{
    
}