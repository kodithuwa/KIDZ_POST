using KIDZ_POST.DATA.CONTRACT;
using KIDZ_POST.DATA.MODEL;
using KIDZ_POST.WEB.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        [HttpGet("GetUserMessages/{teacherId}/{messageId}")]
        public IEnumerable<UserMessageModel> GetUserMessages(int teacherId, int messageId)
        {

                var users = this.context.Set<User>().Where(x => x.TeacherId == teacherId);
                var existusermessages = this.context.Set<UserMessage>().Where(x => x.MessageId == messageId);
                var usersExistMessages = users
                    .Join(existusermessages, o => o.Id, i => i.UserId, (o, i) => new { o.Id, i.MessageId });
                if (usersExistMessages.Any())
                {
                    var usermessages = this.context.Set<User>().Where(x => x.TeacherId == teacherId)
                        .GroupJoin(this.context.Set<UserMessage>().Where(x => x.MessageId == messageId), i => i.Id, o => o.UserId, (o, i) => new { UserId = o.Id, UserFullName = $"{o.FirstName} {o.LastName}", UserMessages = i })
                        .SelectMany(x => x.UserMessages.DefaultIfEmpty(), (o, i) => new { o.UserId, o.UserFullName, UserMessageId = i.Id, i.MessageId, i.ViewedTime });

                }
                else
                {

                        var usersMessages = new List<UserMessageModel>();
                        users.ToList().ForEach(x =>
                        {
                            usersMessages.Add(new UserMessageModel
                            {
                                MessageId = messageId,
                                UserId = x.Id,
                                UserFullName = $"{x.FirstName} {x.LastName}",
                            });
                        });

                }
                return default;
           
        }

        [HttpPost]
        public async Task<bool> SaveUserMessages(IEnumerable<UserMessageModel> userMessages)
        {
            if(userMessages == null || !userMessages.Any())
            {
                return default;
            }

            //var updateMessages = userMessages.Where(x => x.UserMessageId > 0);
            var insertMessages = userMessages.Where(x => x.UserMessageId == 0);
            var saveList = new List<UserMessage>();
            //if (updateMessages.Any())
            //{
            //    var updateMessagesIds = updateMessages.Select(x => x.UserMessageId);
            //    var existMessages = this.context.Set<UserMessage>().Where(x => updateMessagesIds.Contains(x.Id));
            //}

            if (insertMessages.Any())
            {
                insertMessages.ToList().ForEach(x =>
                {
                    saveList.Add(new UserMessage
                    {
                        MessageId = x.MessageId,
                        UserId = x.UserId,
                    });
                });
            }

            if (saveList.Any())
            {
                var savedEntities = new List<EntityEntry<UserMessage>>();
                saveList.ForEach(x =>
                {
                    if (x.Id == 0)
                    {
                        var xx = this.context.Set<UserMessage>().Add(x);
                        savedEntities.Add(xx);
                    }
                    //else
                    //{

                    //}

                });
                var saved = await this.context.SaveAsync();
                if(saved > 0)
                {

                    var result = savedEntities.Select(x => new UserMessageModel
                    {
                        MessageId = x.Entity.MessageId,
                        UserId = x.Entity.UserId,
                        UserMessageId = x.Entity.Id,
                    });

                    return true;
                }
            }

            return false;
        }

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
