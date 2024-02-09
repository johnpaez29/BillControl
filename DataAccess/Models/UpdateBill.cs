using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UpdateBill
    {
        public bool IsPartial { get; set; }
        public BillData Bill { get; set; }
    }
}
