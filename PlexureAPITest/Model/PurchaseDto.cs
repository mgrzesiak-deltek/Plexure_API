using Newtonsoft.Json;

namespace PlexureAPITest.Model
{
    public class PurchaseDto
    {
        [JsonProperty(PropertyName = "product_id")] public int ProductId { get; set; }
    }
}