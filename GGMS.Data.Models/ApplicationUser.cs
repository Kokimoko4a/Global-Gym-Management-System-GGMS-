<<<<<<< Updated upstream
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Models
{
    public class ApplicationUser 
    {
=======
﻿
namespace GGMS.Data.Models
{

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
       


        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


>>>>>>> Stashed changes
    }
}
