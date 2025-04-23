using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace externalPrcatice1.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
    }
}