using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sample.Web.Mvc.Kendo
{
    public class Mapping<TViewModel, TEntityModel> : Dictionary<string, string>
    {
        public void Add( Expression<Func<TViewModel, object>> viewModelProperty, Expression<Func<TEntityModel, object>> entityModelProperty )
        {
            base.Add(viewModelProperty.GetPropertyName(), entityModelProperty.GetPropertyName());
        }
    }

    public class MultiMapping<TViewModel, TEntityModel> : Dictionary<string, List<string>>
    {
        public void Add(Expression<Func<TViewModel, object>> viewModelProperty, List<Expression<Func<TEntityModel, object>>> entityModelProperties)
        {
            var properties = new List<string>();            
            
            entityModelProperties.ForEach(x => properties.Add(x.GetPropertyName()));            
            base.Add(viewModelProperty.GetPropertyName(),properties);
        }
    }

}