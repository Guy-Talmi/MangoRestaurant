using Mango.Services.ShoppingCartApi.Models.Dto;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Repository
{
	public interface ICartRepository
	{
		Task<CartDto> GetCartByUserId(string userId);

		Task<CartDto> CreateUpdateCart(CartDto CartDto);

		Task<bool> RemoveFromCart(int cartDetailsId);

		Task<bool> ClearCart(string userId);
	}
}
