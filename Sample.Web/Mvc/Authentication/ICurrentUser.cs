namespace Sample.Web.Mvc.Authentication
{
    public interface ICurrentUser
    {
        /// <summary>
        /// Get logged user via forms authentication
        /// </summary>
        CurrentUser GetCurrentUser();
    }
}