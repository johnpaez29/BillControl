using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class BillData
    {
        public string Id { get; set; }

        public string IdBill { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime PayDate { get; set; }

        public string State { get; set; }

        public string IdUser { get; set; }
    }
}
