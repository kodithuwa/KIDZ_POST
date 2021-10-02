using KIDZ_POST.DATA.CONTRACT;
using System;
using System.Collections.Generic;

namespace KIDZ_POST.DATA.MODEL
{
    public class Message:IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedTime { get; set; }

        public User CreatedBy { get; set; }

        public IEnumerable<UserMessage> UserMessages { get; set; }

    }
}
