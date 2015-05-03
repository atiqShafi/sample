using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Kendo.Mvc.UI;
using Sample.Core.Db.Context;
using Sample.Core.Db.DbModels;
using Sample.Web.Mvc.Kendo;

namespace Sample.Web.Modules.Users.Builders
{
    /// <summary>
    /// Build users for kendo grid
    /// </summary>
    public class UserGridModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        
        public static Mapping<UserGridModel, User> Mapping = new Mapping<UserGridModel, User>{};
    }
    public class UsersGridBuilder : IGridModelBuilder<UserGridModel>
    {
        private readonly IDbContext _dbContext;

        public UsersGridBuilder(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GridModelResult Build(DataSourceRequest request)
        {
            var result = _dbContext.Get<User>()
                .ToKendoGridResult(request, UserGridModel.Mapping);

            result.Data = ((IEnumerable<User>)result.Data).Select(s => new UserGridModel
            {
                Id = s.Id,
                Created = s.Created,   
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName
            });

            return this.Success(result);

        }
    }
}