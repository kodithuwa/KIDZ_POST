using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.WEB.ApiModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? TeacherId { get; set; }

        public bool IsTeacher { get; set; }

        public bool IsActivated { get; set; }

    }
}
