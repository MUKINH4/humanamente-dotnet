using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace humanamente.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _groqApiKey;

        public AIService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _groqApiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");
        }


        public async Task<string> ClassifyTaskAsync(string description)
        {
            Console.WriteLine("KEY LIDA: " + _groqApiKey);
            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "system", content = "Classifique a tarefa como 'human', 'automatable' ou 'hybrid'." },
                    new { role = "user", content = description }
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.groq.com/openai/v1/chat/completions"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _groqApiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            var classification = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return classification.Trim().ToLower();
        }
    }
}
