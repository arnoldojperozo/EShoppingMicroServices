﻿using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Models;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand, BrandsResponse>().ReverseMap();
    }
}