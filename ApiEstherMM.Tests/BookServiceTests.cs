using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ApiEstherMM.Models;
using ApiEstherMM.Services;
using Moq;
using Moq.Protected;
using Xunit;
using System.Text.Json;

namespace ApiEstherMM.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task SearchBooksAsync_ReturnsBooks()
        {
            // Arrange : on simule une réponse API
            var mockJson = @"{
                ""docs"": [
                    {
                        ""key"": ""/books/1"",
                        ""title"": ""Test Book"",
                        ""author_name"": [""Test Author""],
                        ""cover_i"": 123456
                    }
                ]
            }";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockJson),
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://openlibrary.org")
            };

            var service = new BookService(httpClient);

            // Act
            var results = await service.SearchBooksAsync("test");

            // Assert
            Assert.Single(results);
            Assert.Equal("Test Book", results[0].Title);
            Assert.Equal("Test Author", results[0].Author);
            Assert.Equal("/books/1", results[0].Key);
        }
    }
}