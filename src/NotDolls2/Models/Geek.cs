using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NotDolls2.Models
{
    public class Geek
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
