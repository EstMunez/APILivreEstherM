using System.Collections.Generic;
using ApiEstherMM.Models;

namespace ApiEstherMM.Services
{
    public class FavoriteService
    {
        private readonly List<Book> _favorites = new();

        public IReadOnlyList<Book> GetFavorites() => _favorites;

        public void AddToFavorites(Book book)
        {
            if (!_favorites.Any(f => f.Key == book.Key))
                _favorites.Add(book);
        }

        public void RemoveFromFavorites(string key)
        {
            var book = _favorites.FirstOrDefault(f => f.Key == key);
            if (book != null)
                _favorites.Remove(book);
        }

        public bool IsFavorite(string key) => _favorites.Any(f => f.Key == key);
    }
}