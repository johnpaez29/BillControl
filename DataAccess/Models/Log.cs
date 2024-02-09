using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Log
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Change { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
