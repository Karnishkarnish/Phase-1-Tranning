using Day12api.Context;
using Day12api.Model;
using Day12api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("GetAllBooks")]
        public IEnumerable<Book> GetAllBooks()
        {
            return _service.GetAllBooks();
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(Book book) => Ok(_service.AddBook(book));

        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id) => Ok(_service.DeleteBook(id));

        [HttpPost("AddAuthor")]
        public IActionResult AddAuthor(Author author) => Ok(_service.AddAuthor(author));

        [HttpGet("FetchAllNewBook")]
        public IActionResult GetAllNewBooks() => Ok(_service.GetAllNewBooks());

        [HttpPost("AddSalesInfo")]
        public IActionResult AddSalesInfo(SalesEntry entry) => Ok(_service.AddSalesInfo(entry));

        [HttpGet("Joins")]
        public IActionResult Joins() => Ok(_service.GetSalesWithBooks());

        [HttpPost("AddNewBook")]
        public IActionResult AddNewBook(BookAndAuthorEntry entry) => Ok(_service.AddNewBook(entry));
    }
}
