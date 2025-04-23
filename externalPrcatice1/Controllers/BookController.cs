using externalPrcatice1.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace externalPrcatice1.Controllers
{
    public class BookController : ApiController
    {
        private LibraryEntities LibraryEntities = new LibraryEntities();

        [HttpGet]
        [Route("api/Book/GetAllBook")]
        public IHttpActionResult GetAllBook()
        {
            try
            {
                List<BookTbl> booklist = new List<BookTbl>();

                foreach (var item in LibraryEntities.BookTbls)
                {
                    BookTbl book = new BookTbl()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Author = item.Author,
                        TotalCopies = item.TotalCopies,
                    };
                    booklist.Add(book);
                }

                if (booklist.Count == 0)
                {
                    return BadRequest("Not Found");
                    //return Content(HttpStatusCode.NotFound,"Not Foun");
                }
                return Ok(booklist);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/Book/AddAllBook")]
        public IHttpActionResult PostBook(BookViewModel book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Book data is null");
                }



                BookTbl bookTbl = new BookTbl()
                {
                    Title = book.Title,
                    Author = book.Author,
                    TotalCopies = book.TotalCopies
                };

                LibraryEntities.BookTbls.Add(bookTbl);
                LibraryEntities.SaveChanges();
                return Ok("Record Inserted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("api/Book/UpdateAllBook")]
        public IHttpActionResult PutBook([FromBody] BookViewModel book, [FromUri] int? Id)
        {
            if (Id==null)
            {
                return BadRequest("Id Not Found");
            }
            var find = LibraryEntities.BookTbls.Find(Id);

            if (book == null)
            {
                return BadRequest("Book Data not found");
            }

            if (find == null)
            {
                return NotFound();
            }

            find.Title = book.Title;
            find.Author = book.Author;
            find.TotalCopies = book.TotalCopies;

            LibraryEntities.SaveChanges();

            return Ok("Updated");
        }

        [HttpDelete]
        [Route("api/Book/DeleteBook")]
        public IHttpActionResult DeleteBook([FromUri] int Id)
        {
            var find = LibraryEntities.BookTbls.Find(Id);

            LibraryEntities.BookTbls.Remove(find);

            LibraryEntities.SaveChanges();
            return Ok("Deleted");
        }

        

    }
}
