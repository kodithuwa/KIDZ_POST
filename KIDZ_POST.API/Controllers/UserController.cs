namespace KIDZ_POST.API.Controllers
{
    using KIDZ_POST.API.ApiModels;
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

        [HttpGet("GetAll")]
        public dynamic GetAll()
        {
            var users = this.context.Set<User>().Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.IsTeacher,
                x.UserName,
                x.Password,
                Messages = x.Messages.Select(y => new
                {
                    y.Id,
                    y.Title,
                    y.Body,
                    y.CreatedTime,
                })
            });
            return users;
        }

        [HttpGet("{userId}")]
        public UserModel Get(int userId)
        {
            var user = this.context.Set<User>().FirstOrDefault(x => x.Id == userId);
            if (user == null)
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
                IsActivated = entity.IsActivated
            };
            return result;
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
                IsActivated = rst.IsActivated,
            };
            return result;
        }
    }
}
