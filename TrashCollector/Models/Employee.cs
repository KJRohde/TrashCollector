using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int AreaZipCode { get; set; }
        public string Email { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }

    }
}