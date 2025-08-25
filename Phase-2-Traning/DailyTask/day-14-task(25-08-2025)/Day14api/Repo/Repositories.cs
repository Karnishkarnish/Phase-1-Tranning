using Day12api.Context;
using Day12api.Model;
using Microsoft.EntityFrameworkCore;
using static Day12api.Repo.Repositories;

namespace Day12api.Repo
{
    public class Repositories
    {
        public class BookRepository : IBookRepository
        {
            private readonly MyAppDbContext _context;

            public BookRepository(MyAppDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Book> GetAllBooks() => _context.Books.ToList();

            public void AddBook(Book book) => _context.Books.Add(book);

            public Book GetBookById(int id) => _context.Books.Find(id);

            public void DeleteBook(Book book) => _context.Books.Remove(book);

            public IEnumerable<NewBook> GetAllNewBooks() =>
                _context.NewBooks.Include(x => x.author).ToList();

            public void AddAuthor(Author author) => _context.Authors.Add(author);

            public Author GetAuthorByName(string name) =>
                _context.Authors.FirstOrDefault(x => x.AuthorName == name);

            public void AddNewBook(NewBook newBook) => _context.NewBooks.Add(newBook);

            public NewBook GetNewBookByTitle(string title) =>
                _context.NewBooks.FirstOrDefault(x => x.Title == title);

            public void AddSales(Sales sales) => _context.Sales.Add(sales);

            public IEnumerable<object> GetSalesWithBooks() =>
                _context.Sales.Join(
                    _context.NewBooks,
                    sales => sales.BookId,
                    books => books.NewBookId,
                    (sale, book_group) => new
                    {
                        Copies = sale.num_of_copies,
                        Year = sale.Year,
                        Book = book_group
                    }).ToList();

            public void Save() => _context.SaveChanges();
        }
    }
}
