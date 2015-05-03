using System.Linq;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Web.Modules.Users.Handlers;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Extensions;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Modules.Users.Builders
{
    /// <summary>
    /// Build model for edit user
    /// </summary>
    public class EditUserBuilder : IModelBuilder<EditUserModel,int>
    {
        private readonly IDbContext _dbContext;

        public EditUserBuilder(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ModelBuilderResult<EditUserModel> Build(int userid)
        {
            var dbUser = _dbContext.Get<User>()
                .FirstOrDefault(x => x.Id == userid);
            if (dbUser == null)
                return this.NotFound<EditUserModel>("User not found");

            return this.Success(new EditUserModel
            {
                UserId = dbUser.Id,
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName
            });
        }
    }
}