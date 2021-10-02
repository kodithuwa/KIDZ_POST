using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.WEB.ApiModels
{
    public class MessageModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedTime { get; set; }


    }
}
