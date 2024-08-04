using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Dto.Book;

namespace WebApi.Services.Book
{
    public interface IBookInterface
    {
        Task<ResponseModel<List<BookModel>>> ListBooks();
        Task<ResponseModel<BookModel>> SearchBookById(int idBook);
        Task<ResponseModel<List<BookModel>>> SearchBookByIdAuthor(int idBook);
        Task<ResponseModel<List<BookModel>>> CreateBook(BookCreationDto bookCreationDto);
        Task<ResponseModel<List<BookModel>>> EditBook(BookEditionDto bookEditionDto);
        Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook);
    }
}