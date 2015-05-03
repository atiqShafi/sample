using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Common
{
    public interface IModelBuilder<TModel> : IModelBuilderResponse
    {
        ModelBuilderResult<TModel> Build();
    }

    public interface IModelBuilder<TModel, in TInput> : IModelBuilderResponse
    {
        ModelBuilderResult<TModel> Build(TInput input);
    }

    public interface IModelBuilderResponse { }
}
