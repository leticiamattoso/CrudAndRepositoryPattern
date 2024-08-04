using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }

    }
}