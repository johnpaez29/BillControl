using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class BillControl
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<MonthControl> Months { get; set; }

    }

    public class MonthControl
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsPaid { get; set; }
    }
}
