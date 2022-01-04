using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int? Score { get; set; }
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
        public Movie Movie { get; set; }
        [Required]
        public int MovieID { get; set; }

    }
}
