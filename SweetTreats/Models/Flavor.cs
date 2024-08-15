using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetTreats.Models
{
	public class Flavor
	{
		public int FlavorId { get; set; }
		[Required(ErrorMessage = "The flavor requires a name")]
		public string Name { get; set; }
		public List<SweetFlavor> JoinEntities { get; }
		public ApplicationUser User { get; set; }
	}
}