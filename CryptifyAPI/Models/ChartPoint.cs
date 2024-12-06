namespace CryptifyAPI.Models;

public class ChartPoint
{
	public decimal Price { get; set; }
	public DateTime Time { get; set; }

	public ChartPoint(decimal price, DateTime time)
	{
		Price = price;
		Time = time;
	}
}