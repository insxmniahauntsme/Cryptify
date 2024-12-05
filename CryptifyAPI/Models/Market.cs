namespace CryptifyAPI.Models;

public class Market
{
	public string Name { get; set; }
	public string Base { get; set; }
	public string Target { get; set; }
	public decimal Price { get; set; }
	public string TradeUrl { get; set; }
}