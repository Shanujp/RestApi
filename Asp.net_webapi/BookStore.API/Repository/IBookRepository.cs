using BookStore.API.Models;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModels>> GetAllBooksAsync();
        Task<BookModels> GetBookByIdAsync(int id);
    }
}
