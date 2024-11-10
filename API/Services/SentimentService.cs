using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace API.Services
{
    public class SentimentService : ISentimentService
    {
        private readonly HttpClient _httpClient;
        private const string ScoringUri = "https://textmining.azure-api.net/sentiment_model;rev=2/score"; // Update as needed
        private const string SubscriptionKey = "ca4021eab41244b8858fd0b32b28e33a"; // Replace with your actual subscription key

        public SentimentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetSentimentAsync(string inputText)
        {
            var requestUri = $"{ScoringUri}?input_text={Uri.EscapeDataString(inputText)}"; // Using query parameters
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error calling the sentiment analysis API: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var sentimentResult = JObject.Parse(responseContent);

            // Extract the sentiment value from the dictionary
            var sentiment = sentimentResult["sentiment"]?.ToString();
            if (sentiment == null)
            {
                throw new Exception("The response did not contain a 'sentiment' key.");
            }

            return sentiment;
        }
    }
}
