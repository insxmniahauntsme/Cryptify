namespace CryptifyAPI.Models
{
	public class Currency
	{
		public string Id { get; set; }
		public string Symbol { get; set; } 
		public string Name { get; set; }
		public string Image { get; set; }
		public double CurrentPrice { get; set; }
		public long MarketCap { get; set; }  
		public int MarketCapRank { get; set; }
		public long FullyDilutedValuation { get; set; }  
		public long TotalVolume { get; set; }  
		public double High24H { get; set; }
		public double Low24H { get; set; }
		public double PriceChange24H { get; set; }
		public double PriceChangePercentage24H { get; set; }
		public double MarketCapChange24H { get; set; }
		public double MarketCapChangePercentage24H { get; set; }
		public double CirculatingSupply { get; set; }
		public double TotalSupply { get; set; }
		public double? MaxSupply { get; set; }
		public double Ath { get; set; }
		public double AthChangePercentage { get; set; }
		public DateTime AthDate { get; set; }
		public double Atl { get; set; }
		public double AtlChangePercentage { get; set; }
		public DateTime AtlDate { get; set; }
		public Roi? Roi { get; set; } 
		public DateTime LastUpdated { get; set; }

		public Currency()
		{
		}
	}
}