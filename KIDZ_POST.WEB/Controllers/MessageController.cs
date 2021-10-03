using KIDZ_POST.DATA.CONTRACT;
using KIDZ_POST.DATA.MODEL;
using KIDZ_POST.WEB.ApiModels;
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
        public dynamic Get(int creatorId = 0)
        {
            var messages = creatorId == 0 ? this.context.Set<Message>() : this.context.Set<Message>().Where(x => x.CreatedById == creatorId);
            var result = messages.Select(x => new
            {
                x.Id,
                x.Title,
                x.Body,
                x.CreatedTime,
                x.CreatedById
            });
            return result;
        }

        [HttpGet("{messageId}")]
        public MessageModel GetMessage(int messageId)
        {
            var message = this.context.Set<Message>().FirstOrDefault(x => x.Id == messageId);
            if (message == null)
            {
                return null;
            }
            var result = new MessageModel
            {
                Id = message.Id,
                Title = message.Title,
                Body = message.Body,
                CreatedTime = message.CreatedTime,
                CreatedById = message.CreatedById
            };
            return result;
        }

        [HttpGet("GetUserMessages/{messageId}")]
        public IEnumerable<UserMessageModel> GetUserMessages(int messageId)
        {
            var usermessages = this.context.Set<UserMessage>().Where(x => x.MessageId == messageId);
            if (usermessages == null || !usermessages.Any())
            {
                return null;
            }
            var result = usermessages.Select(x => new UserMessageModel
            {
                MessageId = x.MessageId,
                UserId = x.UserId,
                UserMessageId = x.Id,
                ViewedTime = x.ViewedTime,
            });
                
            return result;
        }


    //    public IEnumerable<UserMessageModel> GetUserMessages(int teacherId, int messageId)
    //    {

    //        var usermessages = this.context.Set<User>().Where(x => x.TeacherId == teacherId)
    //.GroupJoin(this.context.Set<UserMessage>().Where(x => x.MessageId == messageId), i => i.Id, o => o.UserId, (o, i) => new { UserId = o.Id, UserFullName = $"{o.FirstName} {o.LastName}", UserMessages = i })
    //.SelectMany(x => x.UserMessages.DefaultIfEmpty(), (o, i) => new { o.UserId, o.UserFullName, UserMessageId = i.Id, i.MessageId, i.ViewedTime });

    //        if (usermessages == null || !usermessages.Any())
    //        {
    //            return null;
    //        }
    //        var result = usermessages.Select(x => new UserMessageModel
    //        {
    //            MessageId = x.MessageId,
    //            UserId = x.UserId,
    //            UserFullName = x.UserFullName,
    //            UserMessageId = x.UserMessageId,
    //            ViewedTime = x.ViewedTime,
    //        });

    //        return result;
    //    }
        [HttpPost]
        public async Task<MessageModel> Create(MessageModel message)
        {
            var entity = this.context.Set<Message>().Add(new Message { Id = message.Id, Body = message.Body, Title = message.Title, CreatedById = message.CreatedById, CreatedTime = message.CreatedTime });
            var progress = await this.context.SaveAsync();
            var obj = entity.Entity;
            var result = progress > 0 ? new MessageModel
            {
                Id = obj.Id,
                Body = obj.Body,
                Title = obj.Title,
                CreatedTime = obj.CreatedTime,
                CreatedById = obj.CreatedById
            } : null;
            return result;
        }

        [HttpPut]
        public async Task<MessageModel> Update(MessageModel message)
        {
            if (message == null || message.Id == 0)
            {
                return default;
            }
            var entity = this.context.Set<Message>().FirstOrDefault(x => x.Id == message.Id);
            if (entity == null)
            {
                return default;
            }

            entity.Title = message.Title;
            entity.Body = message.Body;
            this.context.Set<Message>().Update(entity);
            var progress = await this.context.SaveAsync();
            var result = progress > 0 ? message : null;
            return result;
        }

        [HttpDelete]
        public async Task<bool> Delete(int messageId)
        {
            var item = this.context.Set<Message>().FirstOrDefault(x => x.Id == messageId);
            var entity = this.context.Set<Message>().Remove(item);
            var progress = await this.context.SaveAsync();
            return progress > 0;
        }


    }
}
