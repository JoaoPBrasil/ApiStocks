
public class BrapiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public BrapiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<StockData>> GetStockDataAsync(string[] symbols)
    {
        var baseUrl = _configuration["Brapi:BaseUrl"];
        var token = _configuration["Brapi:Token"];
        var url = $"{baseUrl}/{string.Join(",", symbols)}?token={token}";

        var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);
        var results = response.GetProperty("results");

        var stockList = new List<StockData>();
        foreach (var item in results.EnumerateArray())
        {
            stockList.Add(new StockData
            {
                Symbol = item.GetProperty("symbol").GetString(),
                LongName = item.GetProperty("longName").GetString(),
                RegularMarketPrice = item.GetProperty("regularMarketPrice").GetDecimal(),
                RegularMarketChangePercent = item.GetProperty("regularMarketChangePercent").GetDecimal(),
                LogoUrl = item.GetProperty("logourl").GetString()
            });
        }

        return stockList;
    }
}
