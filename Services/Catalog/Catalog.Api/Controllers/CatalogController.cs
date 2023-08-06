using System.Net;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #region GET Methods
    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductByName")]
    [ProducesResponseType(typeof(IList<ProductResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductByName(string productName)
    {
        var query = new GetProductsByNameQuery(productName);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var query = new GetAllProductsQuery(catalogSpecParams);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<ProductBrandResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductBrandResponse>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<ProductTypeResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductTypeResponse>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{brandName}", Name = "GetProductsByBrandName")]
    [ProducesResponseType(typeof(IList<ProductResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductsByBrandName(string brandName)
    {
        var query = new GetProductsByBrandQuery(brandName);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("[action]/{typeName}", Name = "GetProductsByTypeName")]
    [ProducesResponseType(typeof(IList<ProductResponse>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductsByTypeName(string typeName)
    {
        var query = new GetProductsByTypeQuery(typeName);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    #endregion
    
    #region POST Methods
    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);

        return Ok(result);
    }
    #endregion

    #region PUT Methods
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);

        return Ok(result);
    }
    #endregion

    #region DELETE Methods
    [HttpDelete]
    [Route("{id}",Name="DeleteProduct")]
    [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var productCommand = new DeleteProductByIdCommand(id);
        
        var result = await _mediator.Send(productCommand);

        return Ok(result);
    }
    #endregion
}