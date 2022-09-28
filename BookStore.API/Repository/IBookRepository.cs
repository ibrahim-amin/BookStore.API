using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBookAsync();
        Task<BookModel> GetBookById(int BookId);
        Task<int> AddBook(BookModel bookModel);
        Task UpdateBook(int Id, BookModel bookModel);
        Task UpdatePatchBookAsync(int Id, JsonPatchDocument bookModel);
        Task DeleteBookAsync(int BookId);
    }
}
