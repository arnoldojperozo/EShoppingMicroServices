using MediatR;

namespace Discount.Application.Commands;

public class CreateDiscountCommand : IRequest<CouponModel>
{
    public string ProductName { get; set; }
    public string Amount { get; set; }
    public string Description { get; set; }
}
