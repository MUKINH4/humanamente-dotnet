using humanamente.Services;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace humanamente.Testes
{
    public class AIServiceTests
    {
        private IConfiguration BuildConfig()
        {
            var settings = new Dictionary<string, string>
            {
                { "Groq:ApiKey", "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" }
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }

        [Fact]
        public async Task ClassifyTaskAsync_Should_Return_NonEmpty_String()
        {
            // Arrange
            var httpClient = new HttpClient(); // O ideal é usar mock com HttpMessageHandler
            IConfiguration config = BuildConfig();

            var service = new AIService(httpClient, config);
            var description = "Tarefa qualquer de exemplo";

            // Act
            var result = await service.ClassifyTaskAsync(description);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public async Task ClassifyTaskAsync_Should_Return_Allowed_Category()
        {
            // Arrange
            var httpClient = new HttpClient();
            IConfiguration config = BuildConfig();

            var service = new AIService(httpClient, config);
            var description = "Atendimento com empatia ao usuário";

            // Act
            var result = await service.ClassifyTaskAsync(description);

            // Assert
            var allowed = new[] { "human", "automatable", "pending" };
            Assert.Contains(result, allowed);
        }
    }
}
