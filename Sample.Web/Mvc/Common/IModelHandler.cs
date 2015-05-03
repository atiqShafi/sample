using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Common
{
    public interface IModelHandler<in TInput> : IModelHandlerResponse
    {
        ModelHandlerResult Handle(TInput input);
    }

    public interface IModelHandler<in TInput, in THandler> : IModelHandlerResponse
    {
        ModelHandlerResult Handle(TInput input);
    }

    public interface IModelHandler : IModelHandlerResponse
    {
        ModelHandlerResult Handle();        
    }

    public interface IModelHandlerResponse { }

}