using System.Linq;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.Users.Handlers
{
    /// <summary>
    /// Handle delete user
    /// </summary>
    public class DeleteUserHandler : IModelHandler<int,DeleteUserHandler>
    {
        private readonly IDbContext _dbContext;

        public DeleteUserHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelHandlerResult Handle(int userId)
        {
            var dbUser = _dbContext.Get<User>()
                .FirstOrDefault(x => x.Id == userId);
            if (dbUser == null)
                return this.Error("User not found");

            if (dbUser.Email == "admin@sample.cz")
                return this.Error("Cannot delete default account");
            _dbContext.Remove(dbUser);
            _dbContext.SaveChanges();

            return this.Success("User deleted successfully");
        }
    }
}