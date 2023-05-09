using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;
namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context) 
        {
           _context = context;
        }
        public async Task<List<BookModels>>GetAllBooksAsync()
        {
            // var records = _context.Books.ToListAsync();//storing entire data of books table to record variable
            //convert this books  to bookmodel
            var records =await _context.Books.Select(x=>new BookModels()
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
            }
            ).ToListAsync();


            return records;
        }

       
        public async Task<BookModels> GetBookByIdAsync(int id)
        {
            var record = await _context.Books.Where(x => x.Id == id)
                .Select(x => new BookModels()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Title = x.Title,
                })
                .FirstOrDefaultAsync();

                return record;
        }


    }
}
