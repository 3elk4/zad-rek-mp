using MediportaZadRek.Data.JsonModels;
using MediportaZadRek.Models;
using Newtonsoft.Json;
using System.Net;

namespace MediportaZadRek.Data
{
    public static class TagsFromSOApiCollector
    {
        private static readonly string BASE_URL = "https://api.stackexchange.com/2.3/tags";
        private static readonly int PAGE_SIZE = 100;
        private static readonly string ORDER = "desc";
        private static readonly string SORT = "popular";
        private static readonly string SITE = "stackoverflow";
        private static readonly string STATIC_URL_PARAMS = $"pagesize={PAGE_SIZE}&order={ORDER}&sort={SORT}&site={SITE}";

        public static async Task<List<Tag>> CollectAsync()
        {
            List<Tag> tags = new List<Tag>();

            HttpClientHandler clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };

            using (HttpClient client = new HttpClient(clientHandler) { BaseAddress = new Uri(BASE_URL) })
            {
                for (int i = 1; i <= 10; i++)
                {
                    var result = await GetTagsFromApiByPageAsync(client, i);
                    var deserializedTags = Deserialize(result);

                    if (deserializedTags != null)
                    {
                        tags.AddRange(deserializedTags);
                    }
                    else
                    {
                        var error = DeserializeAsError(result);
                        //throw new InvalidResponseException($"Invalid response with status code {error.StatusCode}. Cause: {error.Message}.");
                    }
                }
            }

            return tags;
        }

        private static List<Tag>? Deserialize(string data)
        {
            RootTag? root = JsonConvert.DeserializeObject<RootTag>(data);
            return root?.Items;
        }

        private static Error DeserializeAsError(string data)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<Error>(data);
#pragma warning restore CS8603 // Possible null reference return.
        }

        private static async Task<string> GetTagsFromApiByPageAsync(HttpClient client, int page)
        {
            var urlParams = $"?page={page}&{STATIC_URL_PARAMS}";

            using (HttpResponseMessage response = await client.GetAsync(urlParams))
            {
                using (HttpContent content = response.Content)
                {
                    return await content.ReadAsStringAsync();
                }
            }
        }
    }
}
