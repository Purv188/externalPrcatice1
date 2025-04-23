using externalPrcatice1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace externalPrcatice1.Controllers
{
    public class IssueController : ApiController
    {
        private LibraryEntities LibraryEntities = new LibraryEntities();

        [HttpGet]
        [Route("api/Issue/GetAll")]
        public IHttpActionResult GetAll()
        {

            List<IssueViewModel> IssueList = new List<IssueViewModel>();

            foreach (var item in LibraryEntities.IssueTbls)
            {
                IssueViewModel issueView = new IssueViewModel()
                {
                    BookID = item.BookID,
                    Id = item.Id,
                    IssuedDate = item.IssuedDate,
                    IssuedTo = item.IssuedTo,
                    ReturnDate = item.ReturnDate

                };
                IssueList.Add(issueView);
            }

            return Ok(IssueList);
        }

        [HttpGet]
        [Route("api/Issue/GetByDate")]
        public IHttpActionResult GetByDate([FromUri] DateTime start, [FromUri] DateTime end)
        {
            if (start > end)
            {
                return BadRequest("Star Date must be Smaller than end");
            }

            List<IssueViewModel> list = new List<IssueViewModel>();

            foreach (var issue in LibraryEntities.IssueTbls)
            {
                if (issue.IssuedDate >= start && issue.IssuedDate <= end)
                {
                    IssueViewModel issueView = new IssueViewModel()
                    {
                        Id = issue.Id,
                        BookID = issue.BookID,
                        IssuedTo = issue.IssuedTo,
                        IssuedDate = issue.IssuedDate,
                        ReturnDate = issue.ReturnDate

                    };
                    list.Add(issueView);

                }
            }

            if (list.Count == 0)
            {
                return NotFound();
            }

            return Ok(list);

        }

        [HttpPost]
        [Route("api/Issue/PostIssue")]
        public IHttpActionResult PostIssue([FromBody] IssueViewModel issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All Filed are Required");
            }
            if (issue == null)
            {
                return BadRequest("Null");
            }
            if (issue.ReturnDate < issue.IssuedDate)
            {
                return BadRequest("Return Date must be after the Issue Date.");
            }
            var f = LibraryEntities.BookTbls.Any(data => data.Id == issue.BookID);

            if (!f)
            {
                return BadRequest("Not a Valid Book Id");
            }

            IssueTbl issueTbl = new IssueTbl()
            {
                BookID = issue.BookID,
                IssuedTo = issue.IssuedTo,
                IssuedDate = issue.IssuedDate,
                ReturnDate = issue.ReturnDate
            };

            LibraryEntities.IssueTbls.Add(issueTbl);

            LibraryEntities.SaveChanges();

            return Ok("Record Inserted");
        }


        [HttpPut]
        [Route("api/Issue/PutIssue")]
        public IHttpActionResult PutIssue([FromBody] IssueViewModel issue, [FromUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Null Not Allowed");
            }
            if (issue == null)
            {
                return BadRequest("Null");
            }
            if (issue.ReturnDate < issue.IssuedDate)
            {
                return BadRequest("Return Date must be after the Issue Date.");
            }
            var find = LibraryEntities.IssueTbls.Find(id);

            if (find == null)
            {
                return BadRequest("Id not found");
            }

            var f = LibraryEntities.BookTbls.Any(data => data.Id == issue.BookID);
            if (!f)
            {
                return BadRequest("Not a Valid Book ID");
            }


            find.BookID = issue.BookID;
            find.IssuedTo = issue.IssuedTo;
            find.IssuedDate = issue.IssuedDate;
            find.ReturnDate = issue.ReturnDate;

            LibraryEntities.SaveChanges();

            return Ok("Record Updated");
        }

        [HttpDelete]
        [Route("api/Issue/Delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var find = LibraryEntities.IssueTbls.Find(id);
            if (find == null)
            {
                return BadRequest("Not Found");
            }

            LibraryEntities.IssueTbls.Remove(find);

            LibraryEntities.SaveChanges();

            return Ok("Deleted");
        }

    }
}
