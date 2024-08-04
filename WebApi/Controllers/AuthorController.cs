using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Data.Models;
using WebApi.Dto.Author;
using WebApi.Services.Author;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorInterface;
        public AuthorController(IAuthorInterface authorInterface)
        {
            _authorInterface = authorInterface;
        }

        [HttpGet("ListAuthors")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> ListAuthors()
        {
            var authors = await _authorInterface.ListAuthors();
            return Ok(authors);
        }

        [HttpGet("SearchAuthorById/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> SearchAuthorById(int idAuthor)
        {
            var author = await _authorInterface.SearchAuthorById(idAuthor);
            return Ok(author);
        }

        [HttpGet("SearchAuthorByIdBook/{idBook}")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> SearchAuthorByIdBook(int idBook)
        {
            var author = await _authorInterface.SearchAuthorByIdBook(idBook);
            return Ok(author);
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> CreateAuthor(AuthorCreationDto authorCreationDto)
        {
            var authors = await _authorInterface.CreateAuthor(authorCreationDto);
            return Ok(authors);
        }

        [HttpPut("EditAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> EditAuthor(AuthorEditionDto authorEditionDto)
        {
            var authors = await _authorInterface.EditAuthor(authorEditionDto);
            return Ok(authors);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> DeleteAuthor(int idAuthor)
        {
            var authors = await _authorInterface.DeleteAuthor(idAuthor);
            return Ok(authors);
        }
    }
}