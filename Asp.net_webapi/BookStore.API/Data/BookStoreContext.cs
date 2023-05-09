using BookStore.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)      //this way we configure dbcontext
        {
        }
        public DbSet<Books>Books{get;set;} //create new table in dB



    }
}
