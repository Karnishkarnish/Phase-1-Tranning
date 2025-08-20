using Day12api.Context;
using Day12api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day12api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly MyAppDbContext _appDbContext;

        public BookController(MyAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // ✅ 1. Sample 10+ books with Price
        [HttpGet("GetAllBooks")]
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishedDate = new DateTime(1925, 4, 10), Genre = "Fiction", Price = 10.99m },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedDate = new DateTime(1960, 7, 11), Genre = "Fiction", Price = 7.99m },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", PublishedDate = new DateTime(1949, 6, 8), Genre = "Dystopian", Price = 8.99m },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", PublishedDate = new DateTime(1813, 1, 28), Genre = "Romance", Price = 6.99m },
                new Book { Id = 5, Title = "Moby Dick", Author = "Herman Melville", PublishedDate = new DateTime(1851, 11, 14), Genre = "Adventure", Price = 12.50m },
                new Book { Id = 6, Title = "The Catcher in the Rye", Author = "J.D. Salinger", PublishedDate = new DateTime(1951, 7, 16), Genre = "Fiction", Price = 9.75m },
                new Book { Id = 7, Title = "War and Peace", Author = "Leo Tolstoy", PublishedDate = new DateTime(1869, 1, 1), Genre = "Historical", Price = 15.20m },
                new Book { Id = 8, Title = "The Hobbit", Author = "J.R.R. Tolkien", PublishedDate = new DateTime(1937, 9, 21), Genre = "Fantasy", Price = 11.40m },
                new Book { Id = 9, Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", PublishedDate = new DateTime(1866, 1, 1), Genre = "Psychological", Price = 13.80m },
                new Book { Id = 10, Title = "The Alchemist", Author = "Paulo Coelho", PublishedDate = new DateTime(1988, 1, 1), Genre = "Philosophical", Price = 14.00m }
            };

            return books;
        }

        // ✅ Add book
        [HttpPost("AddBook")]
        public IActionResult AddBook(Book book)
        {
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return Ok("Book added successfully");
        }

        // ✅ Delete book
        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _appDbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            _appDbContext.Books.Remove(book);
            _appDbContext.SaveChanges();
            return Ok("Book deleted successfully");
        }

        // ✅ 1.1 GetBooksOrderedByPrice (sort ASC/DESC based on bool)
        [HttpPost("GetBooksOrderedByPrice")]
        public IActionResult GetBooksOrderedByPrice([FromQuery] bool ascending)
        {
            var books = ascending
                ? _appDbContext.Books.OrderBy(b => b.Price).ToList()
                : _appDbContext.Books.OrderByDescending(b => b.Price).ToList();

            return Ok(books);
        }

        // ✅ 1.2 GetTop5Books (costliest books)
        [HttpGet("GetTop5Books")]
        public IActionResult GetTop5Books()
        {
            var books = _appDbContext.Books
                .OrderByDescending(b => b.Price)
                .Take(5)
                .ToList();

            return Ok(books);
        }

        // ✅ 1.3 GetCostDetails (Total & Average Price)
        [HttpGet("GetCostDetails")]
        public IActionResult GetCostDetails()
        {
            var total = _appDbContext.Books.Sum(b => b.Price);
            var average = _appDbContext.Books.Average(b => b.Price);

            return Ok(new
            {
                TotalPrice = total,
                AveragePrice = average
            });
        }
    }
}
