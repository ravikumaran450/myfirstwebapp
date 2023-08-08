using System;
using System.Collections.Generic;

namespace MYWebApplication.Models
{
    public partial class LoginUser
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public int ConfirmPassword {get; set;}

        public int Password { get; set; }
     
        public string Role { get; set; }

    }
}
