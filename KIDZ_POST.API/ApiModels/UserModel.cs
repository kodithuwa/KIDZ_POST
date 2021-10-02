using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.API.ApiModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActivated { get; set; }

    }
}
