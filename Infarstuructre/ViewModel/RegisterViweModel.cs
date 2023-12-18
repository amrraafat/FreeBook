using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
  public class RegisterViweModel
    {
        public List <VwUser> Users { get; set; }
        public NewRegister NewRegister { get; set; }
        public List <IdentityRole> Roles { get; set; }


    }
   public class NewRegister
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData),ErrorMessageResourceName = "RegisterName")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        public string Name { get; set; }
        
        public string RoleName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmailError")]
        public string Email { get; set; }
        public string ImgeUser { get; set; }
        public bool AciveUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "Password")]
        [MinLength(5, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLengthPassword")]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparePassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparePasswordError")]
        public string ComperPassword { get; set; }


    }
}
