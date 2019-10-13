using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Ntl.TappyService.Tests.Utils.ScoreService
{
    public class ScoreServiceClient
    {
        private readonly Uri _serviceUri;
        private readonly HttpClient _httpClient;

        public ScoreServiceClient(string serviceUri)
        {
            _serviceUri = new Uri(serviceUri);
            _httpClient = new HttpClient();
        }

        public async Task<ScoreItem[]> GetScore()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<ScoreItem>));
            UriBuilder uriBuilder = new UriBuilder(_serviceUri)
            {
                Path = "/score_get.php"
            };
            var streamTask = _httpClient.GetStreamAsync(uriBuilder.Uri);
            return serializer.ReadObject(await streamTask) is List<ScoreItem> scoreItems ? scoreItems.ToArray() : 
                new ScoreItem[0];
        }
    }
}