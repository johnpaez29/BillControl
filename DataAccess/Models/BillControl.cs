﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class BillControl
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public int PayDay { get; set; }
        [Required]

        public string State { get; set; }
        [Required]

        public string IdUser { get; set; }


    }
}
