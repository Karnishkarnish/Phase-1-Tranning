using Day12api.Model;
using Day12api.Repo;
using Day12api.Repo;
using System.Collections.Generic;

namespace Day12api.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Book> GetAllBooks() => _repo.GetAllBooks();

        public string AddBook(Book book)
        {
            _repo.AddBook(book);
            _repo.Save();
            return "Book added successfully";
        }

        public string DeleteBook(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null) return "Book not found";

            _repo.DeleteBook(book);
            _repo.Save();
            return "Book deleted successfully";
        }

        public string AddAuthor(Author author)
        {
            _repo.AddAuthor(author);
            _repo.Save();
            return "Author added successfully";
        }

        public IEnumerable<NewBook> GetAllNewBooks() => _repo.GetAllNewBooks();

        public string AddNewBook(BookAndAuthorEntry entry)
        {
            var author = _repo.GetAuthorByName(entry.AuthorName);
            if (author == null) return "Invalid author name";

            NewBook newBook = new NewBook
            {
                Title = entry.BookTitle,
                AuthorId = author.AuthorId,
                price = entry.Price
            };

            _repo.AddNewBook(newBook);
            _repo.Save();
            return "New book added successfully";
        }

        public string AddSalesInfo(SalesEntry entry)
        {
            var book = _repo.GetNewBookByTitle(entry.Book_name);
            if (book == null) return "Invalid book name";

            Sales sales = new Sales
            {
                BookId = book.NewBookId,
                num_of_copies = entry.num_of_copies,
                Year = entry.Year
            };

            _repo.AddSales(sales);
            _repo.Save();
            return "Sales information added successfully";
        }

        public IEnumerable<object> GetSalesWithBooks() => _repo.GetSalesWithBooks();
    }
}
