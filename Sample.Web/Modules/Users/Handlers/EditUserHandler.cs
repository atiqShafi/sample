using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.Users.Handlers
{
    public class EditUserModel
    {
        public int UserId { get; set; }        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }        
    }
    /// <summary>
    /// Handle edit user reqeuest
    /// </summary>
    public class EditUserHandler : IModelHandler<EditUserModel>
    {
        private readonly IDbContext _dbContext;

        public EditUserHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelHandlerResult Handle(EditUserModel model)
        {
            var dbUser = _dbContext.Get<User>()
                .FirstOrDefault(x => x.Id == model.UserId);
            
            if (dbUser == null)
                return this.Error("User not found");

            if (_dbContext.Get<User>().Any(x => x.Email == model.Email && x.Id != dbUser.Id))
                return this.Error("email", "E-mail already exists");

            dbUser.Email = model.Email;
            dbUser.FirstName = model.FirstName;
            dbUser.LastName = model.LastName;

            _dbContext.Update(dbUser);
            _dbContext.SaveChanges();

            return this.Success("User edited successfully");

        }

    }
}