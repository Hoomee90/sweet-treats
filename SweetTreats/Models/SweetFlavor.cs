namespace SweetTreats.Models
{
	public class SweetFlavor
	{
		public int SweetFlavorId { get; set; }
		public int SweetId { get; set; }
		public Sweet Sweet { get; set; }
		public int FlavorId { get; set; }
		public Flavor Flavor { get; set; }
	}
}