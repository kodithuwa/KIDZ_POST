using KIDZ_POST.DATA.CONTRACT;
using System;
using System.Collections.Generic;

namespace KIDZ_POST.DATA.MODEL
{
    public class User:IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActivated { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<UserMessage> UserMessages { get; set; }
    }
}
