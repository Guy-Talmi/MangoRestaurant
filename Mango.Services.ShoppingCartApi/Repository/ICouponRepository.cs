using Mango.Services.ShoppingCartApi.Models.Dto;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
