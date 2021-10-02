using KIDZ_POST.DATA.CONTRACT;
using KIDZ_POST.DATA.MODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIDZ_POST.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> logger;
        private readonly IStorageContext context;

        public MessageController(ILogger<MessageController> logger, IStorageContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var users = this.context.Set<Message>();
            return users;
        }

        [HttpPost]
        public async Task<Message> Create(Message user)
        {
            var entity = this.context.Set<Message>().Add(user);
            var progress = await this.context.SaveAsync();
            var result = progress > 0 ? entity.Entity : null;
            return result;
        }
    }
}
