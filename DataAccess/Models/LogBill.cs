using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class LogBill : Log
    {
        public string IdBill { get; set; }

        [Required]
        public string IdUser { get; set; }
    }
}
