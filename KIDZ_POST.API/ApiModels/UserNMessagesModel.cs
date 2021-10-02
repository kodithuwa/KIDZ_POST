using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.API.ApiModels
{
    public class UserNMessagesModel:UserModel
    {
        public IEnumerable<MessageModel> Messages { get; set; }

    }
}
