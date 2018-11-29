using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MilkVitaOrganization.Models
{
    public class LoginModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public string userId { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool RememberMe { get; set; }

    }
}