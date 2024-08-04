using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Dto.Author;

namespace WebApi.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseModel<List<AuthorModel>>> ListAuthors();
        Task<ResponseModel<AuthorModel>> SearchAuthorById(int idAuthor);
        Task<ResponseModel<AuthorModel>> SearchAuthorByIdBook(int idBook);
        Task<ResponseModel<List<AuthorModel>>> CreateAuthor(AuthorCreationDto authorCreationDto);
        Task<ResponseModel<List<AuthorModel>>> EditAuthor(AuthorEditionDto authorEditionDto);
        Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int idAuthor);
    }
}