using System;
using System.Linq;
using System.Web;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;

namespace Sample.Web.Mvc.Authentication
{
    /// <summary>
    /// Get logged user via forms authentication
    /// </summary>
    public class FormsAuthenticationCurrentUser : ICurrentUser
    {
        private readonly IDbContext _dbContext;

        public FormsAuthenticationCurrentUser(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CurrentUser GetCurrentUser()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            var currentUserId = int.Parse(HttpContext.Current.User.Identity.Name);

            var user = _dbContext.Get<User>().FirstOrDefault(x => x.Id == currentUserId);

            if (user == null)
                return null;

            return new CurrentUser
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName
            };
        }
    }
}