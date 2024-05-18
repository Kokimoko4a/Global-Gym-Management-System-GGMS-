namespace GGMS.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
   

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
              Id = Guid.NewGuid();
        }
    }
}
