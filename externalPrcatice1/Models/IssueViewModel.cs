using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace externalPrcatice1.Models
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="BookId is Required")]
        public int? BookID { get; set; }
        [Required(ErrorMessage = "IssuedTo is Required")]
        public string IssuedTo { get; set; }
        [Required(ErrorMessage = "IssuedDate is Required")]
        public DateTime? IssuedDate { get; set; }
        [Required(ErrorMessage = "ReturnDate is Required")]
        public DateTime? ReturnDate { get; set; }
    }
}