using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Models
{
	public class CartDetails
	{
		[Key]
		public int CartDetailsId { get; set; }

		public int CartHeaderId { get; set; }

		[ForeignKey(nameof(CartHeaderId))]
		public virtual CartHeader CartHeader { get;}

		public int ProductId { get; set; }
		
		[ForeignKey(nameof(ProductId))]
		public virtual Product Product { get; set; }

		public int Count { get; set; }
	}
}
