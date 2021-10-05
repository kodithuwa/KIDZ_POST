namespace KIDZ_POST.WEB.Controllers
{
    using ApiModels;
    using KIDZ_POST.DATA.CONTRACT;
    using KIDZ_POST.DATA.MODEL;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IStorageContext context;

        public UserController(ILogger<UserController> logger, IStorageContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet("GetAll/{teacherId}")]
        public dynamic GetAll(int teacherId = 0)
        {
            var entities = teacherId > 0 ? this.context.Set<User>().Where(x => x.TeacherId == teacherId) : this.context.Set<User>();
            var users = entities.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.UserName,
                x.Password,
                x.TeacherId,
                x.IsActivated,
                x.IsTeacher,
                //Messages = x.Messages.Select(y => new
                //{
                //    y.Id,
                //    y.Title,
                //    y.Body,
                //    y.CreatedTime,
                //})
            });
            return users;
        }

        [HttpGet("{userId}")]
        public UserModel Get(int userId)
        {
            var user = this.context.Set<User>().FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                return null;
            }

            var result = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                IsActivated = user.IsActivated,
                UserName = user.UserName,
                Password = user.Password,
                TeacherId = user.TeacherId,
                IsTeacher = user.IsTeacher
            };
            return result;
        }

        [HttpGet("Login")]
        public UserModel Login([FromQuery] string userName, [FromQuery] string password)
        {
            var entity = this.context.Set<User>().FirstOrDefault(x => x.UserName.ToUpper() == userName.ToUpper() && x.Password.ToUpper() == password.ToUpper());
            if (entity == null)
            {
                return default;
            }

            var result = new UserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Password = entity.Password,
                Description = entity.Description,
                IsTeacher = entity.IsTeacher,
                IsActivated = entity.IsActivated
            };
            return result;
        }

        [HttpPut]
        public async Task<UserModel> Update(UserModel user)
        {
            if (user == null || user.Id == 0)
            {
                return default;
            }
            var entity = this.context.Set<User>().FirstOrDefault(x => x.Id == user.Id);
            if (entity == null)
            {
                return default;
            }

            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Description = user.Description;
            entity.IsTeacher = user.IsTeacher;
            entity.TeacherId = user.TeacherId;
            this.context.Set<User>().Update(entity);
            var progress = await this.context.SaveAsync();
            var result = progress > 0 ? user : null;
            return user;
        }

        [HttpPost("Register")]
        public async Task<UserModel> Register(UserModel user)
        {

            var entity = this.context.Set<User>().Add(new DATA.MODEL.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                UserName = user.UserName,
                Password = user.Password,
                TeacherId = user.TeacherId,
                IsTeacher = user.IsTeacher,
                IsActivated = user.IsActivated,
            });
            var progress = await this.context.SaveAsync();
            var rst = progress > 0 ? entity.Entity : null;
            if (rst == null)
            {
                return null;
            }

            var result = new UserModel
            {
                Id = rst.Id,
                FirstName = rst.FirstName,
                LastName = rst.LastName,
                Description = rst.Description,
                UserName = rst.UserName,
                Password = rst.Password,
                TeacherId = rst.TeacherId,
                IsTeacher = rst.IsTeacher,
                IsActivated = rst.IsActivated,
            };
            return result;
        }
    }
}
