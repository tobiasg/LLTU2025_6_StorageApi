using System.Text.Json.Serialization;

namespace LLTU2025_6_StorageApi.Models.DTO;

public class ProductStatsDto
{
    [JsonPropertyName("totalCount")]
    public int Count { get; set; }

    [JsonPropertyName("totalValue")]
    public double Value { get; set; }

    [JsonPropertyName("averagePrice")]
    public double AveragePrice { get; set; }
}
