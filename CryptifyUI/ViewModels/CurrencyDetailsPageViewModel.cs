using System.Collections.ObjectModel;
using Cryptify.Services;
using CryptifyAPI.Models;
using CryptifyAPI.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;

namespace Cryptify.ViewModels
{
    public class CurrencyDetailsPageViewModel
    {
        public IServiceProvider _serviceProvider { get; set; }
        public INavigationService _navigationProvider { get; set; }
        public ICryptocurrencyService _cryptocurrencyService { get; set; }

        public Currency Currency { get; set; }
        public List<Market> Markets { get; set; }

        public ObservableCollection<ISeries> PriceChangeSeries { get; set; } = new();
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public CurrencyDetailsPageViewModel(IServiceProvider serviceProvider, INavigationService navigationService, ICryptocurrencyService cryptocurrencyService)
        {
            _serviceProvider = serviceProvider;
            _navigationProvider = navigationService;
            _cryptocurrencyService = cryptocurrencyService;
            Currency = new();
            Markets = new();

            InitializeChart();
        }

        private void InitializeChart()
        {
            double low24H = 93213;
            double high24H = 95675;

            var data = Generate24HPriceChange(low24H, high24H);

            var gradient = new LinearGradientPaint(
                new[] { SkiaSharp.SKColors.Blue, SkiaSharp.SKColors.LightBlue },
                new SkiaSharp.SKPoint(0, 0),
                new SkiaSharp.SKPoint(1, 1)
            );

            PriceChangeSeries.Add(new LineSeries<double>
            {
                Values = data,
                Fill = new SolidColorPaint(SkiaSharp.SKColors.Transparent), 
                Stroke = gradient, 
                GeometrySize = 5, 
                GeometryStroke = new SolidColorPaint(SkiaSharp.SKColors.White),
                GeometryFill = new SolidColorPaint(SkiaSharp.SKColors.Blue),
                LineSmoothness = 0.75 
            });

            XAxes = new[]
            {
                new Axis
                {
                    Labels = Enumerable.Range(1, 24).Select(hour => hour + "h").ToArray(),
                    //Name = "Hours",
                    LabelsPaint = new SolidColorPaint(SkiaSharp.SKColors.White),
                    TextSize = 12,
                    NamePaint = new SolidColorPaint(SkiaSharp.SKColors.LightGray),
                    NameTextSize = 14
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    //Name = "Price",
                    LabelsPaint = new SolidColorPaint(SkiaSharp.SKColors.White),
                    TextSize = 12,
                    NamePaint = new SolidColorPaint(SkiaSharp.SKColors.LightGray),
                    NameTextSize = 14
                }
            };
        }

        private static List<double> Generate24HPriceChange(double low, double high)
        {
            var random = new Random();
            var data = new List<double>();

            for (int i = 0; i < 24; i++) 
            {
                double price = random.NextDouble() * (high - low) + low; 
                data.Add(price);
            }

            return data.OrderBy(p => p).ToList(); 
        }
    }
}
