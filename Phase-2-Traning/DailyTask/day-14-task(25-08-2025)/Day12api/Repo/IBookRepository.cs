using Day12api.Model;

namespace Day12api.Repo
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetAllBooks();
       public void AddBook(Book book);
        public void DeleteBook(Book book);
       public IEnumerable<NewBook> GetAllNewBooks();
       public void AddAuthor(Author author);
        public Author GetAuthorByName(string name);
        public void AddNewBook(NewBook newBook);
       public NewBook GetNewBookByTitle(string title);
        public void AddSales(Sales sales);
       public IEnumerable<object> GetSalesWithBooks();
        public Book GetBookById(int id);
        public void Save();
    }
}
