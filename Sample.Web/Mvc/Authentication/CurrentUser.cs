namespace Sample.Web.Mvc.Authentication
{
    /// <summary>
    /// Current logged user
    /// </summary>
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}