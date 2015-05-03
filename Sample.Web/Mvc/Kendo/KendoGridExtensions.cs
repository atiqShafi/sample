using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Sample.Core.Common;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Sample.Web.Mvc.Kendo
{
    public static class KendoGridExtensions
    {
        public static DataSourceRequest Map<TViewModel,TEntityModel>(this DataSourceRequest request,Mapping<TViewModel, TEntityModel> mapping )
        {
            if (request.Sorts != null) 
            {
                // Nový list pro třídění
                var newSortList = new List<SortDescriptor>();

                // Projdeme všechny sloupce, na kterých se má třídit
                foreach (var sortDescription in request.Sorts)
                {
                    sortDescription.Member = sortDescription.Member.ToPascalCase();
                    string propertyInfo;
                    mapping.TryGetValue(sortDescription.Member, out propertyInfo);
                    
                    if (!String.IsNullOrWhiteSpace(propertyInfo))
                    {
                        newSortList.Add(new SortDescriptor(propertyInfo,
                                                           sortDescription.SortDirection));
                    }
                    else
                    {
                        newSortList.Add(sortDescription);
                    }
                }
                // Upravíme DataSourceRequest a vrátíme
                request.Sorts = newSortList;
            }

            if (request.Filters != null)
            {
                var newFilterList = new List<IFilterDescriptor>();

                foreach (var filterDescriptor in request.Filters)
                {
                    if (filterDescriptor is CompositeFilterDescriptor)
                    {
                        var compositeDescriptor = (CompositeFilterDescriptor)filterDescriptor;                       
                        
                        foreach (FilterDescriptor descriptor in compositeDescriptor.FilterDescriptors)
                        {
                            descriptor.Member = descriptor.Member.ToPascalCase();

                             string propertyInfo;

                            mapping.TryGetValue(descriptor.Member, out propertyInfo);
                            if (!String.IsNullOrWhiteSpace(propertyInfo))
                            {
                                newFilterList.Add(new FilterDescriptor(propertyInfo, descriptor.Operator, descriptor.Value));
                            }
                            else
                            {
                                newFilterList.Add(filterDescriptor);
                            }
                        }

                    }
                    else
                    {
                        var descriptor = (FilterDescriptor) filterDescriptor;
                        descriptor.Member = descriptor.Member.ToPascalCase();                  
                        string propertyInfo;
                        mapping.TryGetValue(descriptor.Member, out propertyInfo);
                        if (!String.IsNullOrWhiteSpace(propertyInfo))
                        {
                            newFilterList.Add(new FilterDescriptor(propertyInfo,descriptor.Operator,descriptor.Value));
                        }
                        else
                        {
                            newFilterList.Add(filterDescriptor);
                        }

                    }

                }
                request.Filters = newFilterList;
            }

            return request;
        }

        public static DataSourceRequest Map<TViewModel, TEntityModel>(this DataSourceRequest request, MultiMapping<TViewModel, TEntityModel> mapping)
        {
            if (request.Sorts != null)
            {
                var newSortList = new List<SortDescriptor>();

                foreach (var sortDescription in request.Sorts)
                {
                    var properties = new List<string>();
                    sortDescription.Member = sortDescription.Member.ToPascalCase();
                    newSortList.AddRange(properties.Select(property => new SortDescriptor(property, sortDescription.SortDirection)));
                }
                request.Sorts = newSortList;
            }

            return request;
        }


        public static DataSourceResult ToKendoGridResult<T, TViewModel, TEntityModel>(this IQueryable<T> data, DataSourceRequest request, Mapping<TViewModel, TEntityModel> mapping)
        {
            var mappedRequest = request.Map(mapping);
            return data.ToDataSourceResult(mappedRequest);            
        }

        public static DataSourceResult ToKendoGridResult<T, TViewModel, TEntityModel>(this IQueryable<T> data, DataSourceRequest request, MultiMapping<TViewModel, TEntityModel> mapping)
        {
            var mappedRequest = request.Map(mapping);
            return data.ToDataSourceResult(mappedRequest);
        }

        public static DataSourceResult ToKendoGridResult<T, TViewModel, TEntityModel>(this IQueryable<T> data, DataSourceRequest request, MultiMapping<TViewModel, TEntityModel> mapping, Expression<Func<TViewModel, object>> defaultSorting, ListSortDirection defaultSortingDirection)
        {
            if (request.Sorts == null || request.Sorts.Count == 0) 
            {
                string propertyInfo = defaultSorting.GetPropertyName();

                if (propertyInfo.IsNotEmpty())
                {
                    request.Sorts = new List<SortDescriptor>();
                    request.Sorts.Add(new SortDescriptor(propertyInfo, defaultSortingDirection));
                }
            }

            var mappedRequest = request.Map(mapping);
            return data.ToDataSourceResult(mappedRequest);
        }

        public static DataSourceResult ToKendoGridResult<T, TViewModel, TEntityModel>(this IQueryable<T> data, DataSourceRequest request, Mapping<TViewModel, TEntityModel> mapping, Expression<Func<TViewModel, object>> defaultSorting, ListSortDirection defaultSortingDirection)
        {
            if (request.Sorts == null || request.Sorts.Count == 0) // defaultní sortování
            {
                var propertyInfo = defaultSorting.GetPropertyName(); 
                
                if (propertyInfo.IsNotEmpty())
                {
                    request.Sorts = new List<SortDescriptor> {new SortDescriptor(propertyInfo, defaultSortingDirection)};
                }                
            }

            var mappedRequest = request.Map(mapping);
            return data.ToDataSourceResult(mappedRequest);
        }
        
    }
}
