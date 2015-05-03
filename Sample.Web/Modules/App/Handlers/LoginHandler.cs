using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Core.Infrastructure.Security;
using Sample.Web.Mvc.Authentication;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;
using FormsAuthentication = System.Web.Security.FormsAuthentication;

namespace Sample.Web.Modules.App.Handlers
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    
    /// <summary>
    /// User login handler
    /// </summary>
    public class LoginHandler : IModelHandler<LoginModel>
    {
        private readonly IDbContext _dbContext;

        public LoginHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelHandlerResult Handle(LoginModel model)
        {
            var user = _dbContext.Get<User>()
                .FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
                return this.Error("User not found");
            
            if (!Crypto.VerifyHash(model.Password, user.Password))
                return this.Error("Wrong password");

            FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
            return this.Success("Login success", new CurrentUser
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName
            });
        }
    }
}