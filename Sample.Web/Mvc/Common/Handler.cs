using System.Web.Mvc;

namespace Sample.Web.Mvc.Common
{
    /// <summary>
    /// Handlers loader
    /// </summary>
    public static class Handler
    {
        public static THandler Get<THandler>() where THandler : class
        {
            return DependencyResolver.Current.GetService<THandler>();
        }

    }
}