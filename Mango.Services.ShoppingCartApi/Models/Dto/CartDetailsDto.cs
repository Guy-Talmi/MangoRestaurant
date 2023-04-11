using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Models.Dto
{
	public class CartDetailsDto
	{
		public int CartDetailsId { get; set; }

		public int CartHeaderId { get; set; }

		public virtual CartHeaderDto CartHeader { get;}

		public int ProductId { get; set; }
		
		public virtual ProductDto Product { get; set; }

		public int Count { get; set; }
	}
}
