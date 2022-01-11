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
        public int PayDay { get; set; }

        public string State { get; set; }

        public IEnumerable<MonthControl> Months { get; set; }

    }

    public class MonthControl
    {
        public string Name { get; set; }

        public bool IsPaid { get; set; }
    }
}
