namespace CryptifyAPI.Models;

public class Market
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public decimal Volume { get; set; }
	public string TradeUrl { get; set; }
}