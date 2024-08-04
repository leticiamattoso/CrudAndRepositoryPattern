using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Models;
using WebApi.Dto.Book;

namespace WebApi.Services.Book
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<BookModel>>> CreateBook(BookCreationDto bookCreationDto)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var author = await _context.Authors
                .FirstOrDefaultAsync(authorBase => authorBase.Id == bookCreationDto.Author.Id);

                if (author == null)
                {
                    response.Message = "Nenhum registro de autor localizado!";
                    return response;
                }

                var book = new BookModel()
                {
                    Title = bookCreationDto.Title,
                    Author = author
                };

                _context.Add(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.Include(a => a.Author).ToListAsync();
                response.Message = "Livro criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);

                if (book == null)
                {
                    response.Message = "Nenhum livro localizado!";
                    return response;
                }

                _context.Remove(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.ToListAsync();
                response.Message = "Livro removido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> EditBook(BookEditionDto bookEditionDto)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {

                var book = await _context.Books
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(bookBase => bookBase.Id == bookEditionDto.Id);

                var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBase => authorBase.Id == bookEditionDto.Author.Id);

                if (author == null)
                {
                    response.Message = "Nenhum registro de autor localizado!";
                    return response;
                }

                if (book == null)
                {
                    response.Message = "Nenhum registro de livro localizado!";
                    return response;
                }

                book.Title = bookEditionDto.Title;
                book.Author = author;

                _context.Update(book);
                await _context.SaveChangesAsync();

                response.Data = await _context.Books.ToListAsync();
                response.Message = "Livro editado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> ListBooks()
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var books = await _context.Books.Include(a => a.Author).ToListAsync();

                response.Data = books;
                response.Message = "Todos os livros foram coletados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> SearchBookById(int idBook)
        {
            ResponseModel<BookModel> response = new ResponseModel<BookModel>();

            try
            {
                var books = await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);

                if (books == null)
                {
                    response.Message = "Nenhum registro localizado!";
                    return response;
                }

                response.Data = books;
                response.Message = "Livro localizado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> SearchBookByIdAuthor(int idAuthor)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                var book = await _context.Books
                    .Include(a => a.Author)
                    .Where(bookBase => bookBase.Author.Id == idAuthor)
                    .ToListAsync();
                if (book == null)
                {
                    response.Message = "Nenhum registro localizado!";

                    return response;
                }

                response.Data = book;
                response.Message = "Livros localizados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}