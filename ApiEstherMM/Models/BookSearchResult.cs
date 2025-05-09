using System.Text.Json.Serialization;

namespace ApiEstherMM.Models
{
    public class BookSearchResult
    {
        [JsonPropertyName("docs")]
        public List<BookDoc> Docs { get; set; }
    }

    public class BookDoc
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author_name")]
        public List<string> AuthorName { get; set; }

        [JsonPropertyName("cover_i")]
        public int? CoverId { get; set; }
    }
}