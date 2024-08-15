using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetTreats.Models
{
	public class Sweet
	{
		public int SweetId { get; set; }
		[Required(ErrorMessage = "The sweet requires a name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "The sweet requires a price")]
		public int Price { get; set; }
		[Required(ErrorMessage = "the sweet requires a description")]
		public string Description { get; set; }
		public List<SweetFlavor> JoinEntities { get; }
		public ApplicationUser User { get; set; }
	}
}