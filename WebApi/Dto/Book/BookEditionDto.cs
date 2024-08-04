using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Dto.Book.Bond;

namespace WebApi.Dto.Book
{
    public class BookEditionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorBondDto Author { get; set; }
    }
}