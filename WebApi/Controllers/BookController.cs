using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Data.Models;
using WebApi.Dto.Book;
using WebApi.Services.Book;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookInterface _bookInterface;
        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }

        [HttpGet("ListBooks")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> ListBooks()
        {
            var books = await _bookInterface.ListBooks();
            return Ok(books);
        }

        [HttpGet("SearchAuthorById/{idBook}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> SearchBookById(int idBook)
        {
            var book = await _bookInterface.SearchBookById(idBook);
            return Ok(book);
        }

        [HttpGet("SearchBookByIdAuthor/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> SearchBookByIdAuthor(int idAuthor)
        {
            var book = await _bookInterface.SearchBookByIdAuthor(idAuthor);
            return Ok(book);
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> CreateBook(BookCreationDto bookCreationDto)
        {
            var books = await _bookInterface.CreateBook(bookCreationDto);
            return Ok(books);
        }

        [HttpPut("EditBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> EditAuthor(BookEditionDto bookEditionDto)
        {
            var books = await _bookInterface.EditBook(bookEditionDto);
            return Ok(books);
        }

        [HttpDelete("DeleteBook")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> DeleteAuthor(int idBook)
        {
            var books = await _bookInterface.DeleteBook(idBook);
            return Ok(books);
        }
    }
}