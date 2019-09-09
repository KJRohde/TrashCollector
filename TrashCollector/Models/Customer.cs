using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City")]
        public string City{ get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string ZipCode { get; set; }
        [Display(Name = "Weekly Pickup Day")]
        public string PickupDay { get; set; }
        [Display(Name = "One Time")]
        [DataType(DataType.Date)]
        public string OneTimePickup { get; set; }
        [Display(Name = "Pickup Activity On/Off")]
        public bool PickupActivity { get; set; }
        [Display(Name = "Monthly Bill")]
        public double MonthlyBill { get; set; }
        [Display(Name = "Suspension Start")]
        public string SuspensionStart { get; set; }
        [Display(Name = "Suspension End")]
        public string SuspensionEnd { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public enum DayOfWeek { }
    }
}