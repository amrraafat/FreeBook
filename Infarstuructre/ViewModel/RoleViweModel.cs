using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Resources;


namespace Infarstuructre.ViewModel
{
    //for Get Roles in Viwe  
   public class RoleViweModel
    {
        public List<IdentityRole> Roles { get; set; }

        public NewRole NewRole { get; set; }

    }

    // to Create a New Roles 
    public class NewRole
    {
        public string RoleId { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResourceData),ErrorMessageResourceName ="RoleName")]
        public string RoleName { get; set; }
    }
}
