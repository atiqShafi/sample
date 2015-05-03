using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Core.Infrastructure.Security;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.Users.Handlers
{
    public class CreateUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    /// <summary>
    /// Handle create user request
    /// </summary>
    public class CreateUserHandler : IModelHandler<CreateUserModel>
    {
        private readonly IDbContext _dbContext;

        public CreateUserHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelHandlerResult Handle(CreateUserModel model)
        {
            var dbUser = _dbContext.Get<User>()
                .FirstOrDefault(x => x.Email == model.Email);
            if (dbUser != null)
                return this.Error("email", "E-mail already exists");
            
            _dbContext.Add(new User
            {
                Created = DateTime.Now,
                FirstName = model.FirstName,
                Email = model.Email,
                Password = Crypto.ComputeHash(model.Password),
                LastName = model.LastName
            });
            _dbContext.SaveChanges();
            return this.Success("User created successfully");
        }
    }

}