using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajay.PMS.Models
{
    public class UserLogin : IdentityUser
    {
      public string FirstName { get ; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set;}


        //public string UserUrl {  get; set;}
        
        //[NotMapped]
        //public IFormFile UserFile { get; set;}
        public DateTime CreatedDate { get; set; }


    }
}
