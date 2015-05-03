using Kendo.Mvc.UI;
using Sample.Web.Mvc.Results;

namespace Sample.Web.Mvc.Kendo
{
    public interface IGridModelBuilder<TModel, in TRequestModel> : IGridModelBuilderResponse  
    {
        GridModelResult Build(DataSourceRequest request,TRequestModel requestModel);
    }

    public interface IGridModelBuilder<TModel> : IGridModelBuilderResponse
    {
        GridModelResult Build(DataSourceRequest request);        
    }

    public interface IGridModelBuilderResponse {}
}