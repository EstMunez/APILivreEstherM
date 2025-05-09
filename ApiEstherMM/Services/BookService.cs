using ApiEstherMM.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiEstherMM.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> SearchBooksAsync(string term)
        {
            var url = $"https://openlibrary.org/search.json?q={Uri.EscapeDataString(term)}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<Book>();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BookSearchResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Docs?.Select(doc => new Book
            {
                Key = doc.Key,
                Title = doc.Title,
                Author = doc.AuthorName?.FirstOrDefault() ?? "Inconnu",
                CoverUrl = doc.CoverId.HasValue ? $"https://covers.openlibrary.org/b/id/{doc.CoverId}-M.jpg" : null
            }).ToList() ?? new List<Book>();
        }
    }
}