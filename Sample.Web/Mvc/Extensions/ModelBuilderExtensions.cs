using System.Net;
using Sample.Web.Mvc.Common;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Extensions
{
    /// <summary>
    /// Helpers for model builders
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static ModelBuilderResult<TModel> Success<TBuilder, TModel>(this TBuilder builder, TModel model) where TBuilder : IModelBuilderResponse
        {
            return new ModelBuilderResult<TModel> { HttpStatusCode = HttpStatusCode.OK, Model = model };
        }

        public static ModelBuilderResult<TModel> NotFound<TModel>(this IModelBuilderResponse modelBuilder, string error)
        {
            return new ModelBuilderResult<TModel>{HttpStatusCode = HttpStatusCode.NotFound,Message = error};
        }

        public static FileBuilderResult Success<TBuilder>(this TBuilder builder, string path, string contentType, string fileName) where TBuilder : IFileModelBuilderResponse
        {
            return new FileBuilderResult { FileName = fileName, ContentType = contentType, Path = path };
        }

        public static FileBuilderResult FileNotFound<TBuilder>(this TBuilder builder, string message) where TBuilder : IFileModelBuilderResponse
        {
            return new FileBuilderResult {Message = message};
        }


    }
}