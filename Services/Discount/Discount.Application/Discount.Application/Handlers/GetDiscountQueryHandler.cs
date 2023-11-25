using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQueryHandler, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);

        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with the product name = {request.ProductName} not found"));
        }

        //TODO: Follow Product Mapper kind of Example
        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            Description = coupon.Description,
            Amount = coupon.Amount,
        };

        return couponModel;
    }
}
