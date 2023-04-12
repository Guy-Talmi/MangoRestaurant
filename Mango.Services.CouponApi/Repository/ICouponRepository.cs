using Mango.Services.CouponApi.Models.Dto;

namespace Mango.Services.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
