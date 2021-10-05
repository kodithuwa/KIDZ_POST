using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.WEB.ApiModels
{
    public class UserMessageModel
    {
        public int UserMessageId { get; set; }

        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public MessageModel Message { get; set; }

        public int MessageId { get; set; }

        public DateTime ViewedTime { get; set; }

        public bool IsActivated { get; set; }

    }
}
