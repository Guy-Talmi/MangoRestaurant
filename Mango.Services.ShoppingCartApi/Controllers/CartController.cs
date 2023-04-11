using Mango.Services.ShoppingCartApi.Models.Dto;
using Mango.Services.ShoppingCartApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartApi.Controllers
{
	[ApiController]
	[Route("api/cart")]
	public class CartController : Controller
	{
		private readonly ICartRepository _cartRepository;
		protected ResponseDto _response;

		public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
			_response = new ResponseDto();
		}

		[HttpGet("GetCart/{userId}")]
		public async Task<object> GetCart(string userId)
		{
			try
			{
				CartDto cartDto = await _cartRepository.GetCartByUserId(userId);
				_response.Result = cartDto;

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}

			return _response;
		}
	}
}
