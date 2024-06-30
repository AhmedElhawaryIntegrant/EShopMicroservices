using Discount.grpc.Data;
using Discount.Grpc;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Discount.grpc.Models;
using Mapster;

namespace Discount.grpc.Services
{
    public class DiscountService(DiscountContext discountContext,ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CoupnModel> GetDiscount(DiscountRequest request, ServerCallContext context)
        {
            try
            { 
                var coupon = await discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
                if (coupon == null)
                {
                    coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
                }
                logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");
                var CouponModel = coupon.Adapt<CoupnModel>();

                return CouponModel;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error while retrieving discount for ProductName: {request.ProductName}"));
            }
           
        }

        public override async Task<CoupnModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));
            }
            discountContext.Coupons.Add(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");
            return coupon.Adapt<CoupnModel>();
        }

        public override async Task<CoupnModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));
            }
            discountContext.Coupons.Update(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");
            return coupon.Adapt<CoupnModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));
            }
            discountContext.Coupons.Remove(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully deleted. ProductName: {coupon.ProductName}");
            return new DeleteDiscountResponse { Success = true };
        }


    }
}
