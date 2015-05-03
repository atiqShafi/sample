using System.Web.Mvc;

namespace Sample.Web.Mvc.Common
{
    /// <summary>
    /// Builders loader
    /// </summary>
    public static class Builder
    {
        public static TModelBuilder Get<TModelBuilder>() where TModelBuilder : class
        {
            return DependencyResolver.Current.GetService<TModelBuilder>();
        }

        public static TModelBuilder File<TModelBuilder>()
        {
            return (TModelBuilder)DependencyResolver.Current.GetService<IFileModelBuilder<TModelBuilder>>();
        }

        public static TModelBuilder File<TModelBuilder, TInput>()
        {
            return (TModelBuilder)DependencyResolver.Current.GetService<IFileModelBuilder<TInput>>();
        }

    }
}