using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormTagHelper.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string EmailAdress { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class FruitViewModel
    {
        public string Fruit { get; set; }
        public List<SelectListItem> Fruits { get; set; }
    }

    public class CityEnumViewModel
    {
        public CityEnum EnumCity { get; set; }
    }

    public enum CityEnum
    {
        [Display(Name = "Amdavad")]
        Ahmedabad,
        [Display(Name = "Vadodara")]
        Baroda,
        Gandhinagar,
        Bhavnagar,
        Surat,
        Bharuch,
        Rajkot
    }

    public class CityViewModel
    {
        public string City { get; set; }
        public List<SelectListItem> CityList { get; set; }
    }
}