using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homework_06_05.Data;
using Microsoft.AspNetCore.Authorization;
using Homework_06_05.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;

namespace Homework_06_05.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string _connectionString;

        public AccountController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            var repo = new UserRepository(_connectionString);

            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            return repo.GetByEmail(User.Identity.Name);
        }

        [Route("login")]
        [HttpPost]
        public User LoginUser(LoginViewModel vm)
        {
            var repo = new UserRepository(_connectionString);
            return repo.LoginUser(vm.Email, vm.Password);
        }

        [Route("signup")][HttpPost]
        public void NewUser(SignupViewModel user)
        {
            var repo = new UserRepository(_connectionString);
            repo.NewUser(user, user.Password);
        }

        [Route("logout")][HttpPost]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }
    }
}
