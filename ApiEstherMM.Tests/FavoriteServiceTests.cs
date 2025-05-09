using Xunit;
using ApiEstherMM.Models;
using ApiEstherMM.Services;

namespace ApiEstherMM.Tests
{
    public class FavoriteServiceTests
    {
        [Fact]
        public void AddToFavorites_ShouldAddBook()
        {
            // Arrange
            var service = new FavoriteService();
            var book = new Book { Key = "1", Title = "Test", Author = "Moi" };

            // Act
            service.AddToFavorites(book);

            // Assert
            Assert.Single(service.GetFavorites());
            Assert.True(service.IsFavorite("1"));
        }

        [Fact]
        public void RemoveFromFavorites_ShouldRemoveBook()
        {
            var service = new FavoriteService();
            var book = new Book { Key = "1", Title = "Test" };
            service.AddToFavorites(book);

            service.RemoveFromFavorites("1");

            Assert.Empty(service.GetFavorites());
            Assert.False(service.IsFavorite("1"));
        }

        [Fact]
        public void AddToFavorites_ShouldNotAddDuplicate()
        {
            var service = new FavoriteService();
            var book = new Book { Key = "1", Title = "Test" };

            service.AddToFavorites(book);
            service.AddToFavorites(book); // duplicate

            Assert.Single(service.GetFavorites());
        }
    }
}