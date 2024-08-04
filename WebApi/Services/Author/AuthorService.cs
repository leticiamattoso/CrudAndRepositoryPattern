using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Models;
using WebApi.Dto.Author;

namespace WebApi.Services.Author
{
    public class AuthorService : IAuthorInterface
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCreationDto)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = new AuthorModel()
                {
                    Name = authorCreationDto.Name,
                    Surname = authorCreationDto.Surname
                };

                _context.Add(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Autor criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int idAuthor)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBase => authorBase.Id == idAuthor);

                if (author == null)
                {
                    response.Message = "Nenhum autor localizado!";
                    return response;
                }

                _context.Remove(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Autor removido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> EditAuthor(AuthorEditionDto authorEditionDto)
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(authorBase => authorBase.Id == authorEditionDto.Id);
                if (author == null)
                {
                    response.Message = "Nenhum autor localizado!";
                    return response;
                }

                author.Name = authorEditionDto.Name;
                author.Surname = authorEditionDto.Surname;

                _context.Update(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Autor editado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> ListAuthors()
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

            try
            {
                var authors = await _context.Authors.ToListAsync();

                response.Data = authors;
                response.Message = "Todos os autores foram coletados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> SearchAuthorById(int idAuthor)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();

            try
            {
                var authors = await _context.Authors.FirstOrDefaultAsync(authorBase => authorBase.Id == idAuthor);

                if (authors == null)
                {
                    response.Message = "Nenhum registro localizado!";
                    return response;
                }

                response.Data = authors;
                response.Message = "Autor localizado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthorModel>> SearchAuthorByIdBook(int idBook)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();

            try
            {
                var book = await _context.Books
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(bookBase => bookBase.Id == idBook);
                if (book == null)
                {
                    response.Message = "Nenhum registro localizado!";

                    return response;
                }

                response.Data = book.Author;
                response.Message = "Autor localizado!";
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