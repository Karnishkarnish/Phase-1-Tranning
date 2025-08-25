using Day12api.Model;
using System.Collections.Generic;

namespace Day12api.Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        string AddBook(Book book);
        string DeleteBook(int id);
        string AddAuthor(Author author);
        IEnumerable<NewBook> GetAllNewBooks();
        string AddNewBook(BookAndAuthorEntry entry);
        string AddSalesInfo(SalesEntry entry);
        IEnumerable<object> GetSalesWithBooks();
    }
}
