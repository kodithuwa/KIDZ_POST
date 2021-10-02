using KIDZ_POST.DATA.CONTRACT;
using System;

namespace KIDZ_POST.DATA.MODEL
{
    public class UserMessage:IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MessageId { get; set; }

        public User User { get; set; }

        public Message Message { get; set; }

        public DateTime ViewedTime { get; set; }

    }
}
