using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Common
{
    public interface IFileModelBuilder : IFileModelBuilderResponse
    {
        FileBuilderResult Build();
    }

    public interface IFileModelBuilder<in TInput> : IFileModelBuilderResponse
    {
        FileBuilderResult Build(TInput input);
    }

    public interface IFileModelBuilderResponse { }
}
 