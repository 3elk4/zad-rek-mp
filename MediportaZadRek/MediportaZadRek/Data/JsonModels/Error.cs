using Newtonsoft.Json;

namespace MediportaZadRek.Data.JsonModels
{
    public class Error
    {
        [JsonProperty("error_id")]
        public int StatusCode { get; set; }

        [JsonProperty("error_message")]
        public string Message { get; set; }
    }
}
