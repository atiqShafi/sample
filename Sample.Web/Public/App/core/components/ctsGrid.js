angular.module('app').factory('ctsGrid', function () {
  return {
    create: function (options) {
      var grid = {
        dataSource: {
          type: "aspnetmvc-ajax",
          pageSize: 15,
          transport: {
            read: {
              url: options.url,
              type: 'GET',
              dataType: 'json'
            }
          },
          serverFiltering: true,
          serverPaging: true,
          serverSorting: true,
          schema: {
            data: "data",
            total: "total",
            aggregates: "aggregates"
          },
          sort: options.sort

        },
        groupable: true,
        sortable: true,
        selectable: true,
        pageable: {
          refresh: true,
          pageSizes: [5, 10, 15, 50, 100, 500],
          buttonCount: 5
        },
        filterable: options.filterable,
        columnMenu: true,
        columns: options.columns

      };

      if (options.schema && options.schema.model) {
        grid.dataSource.schema.model = options.schema.model;
      }

      if (options.excelFileName) {
        grid.toolbar = ["excel"];
        grid.excel = {
          fileName: options.excelFileName,
          proxyURL: "/grid-export",
          filterable: true
        };
      }

      // hide columns on init
      if (options.gridOnStart && options.gridOnStart && options.gridOnStart.hiddenColumns) {
        var hiddenColumns = options.gridOnStart.hiddenColumns;
        for (var i = 0; i < grid.columns.length; i++) {
          var isHidden = _.contains(hiddenColumns, grid.columns[i].field);
          if (isHidden) {
            grid.columns[i]['hidden'] = true;
          }
        }
      }

      // add filters
      if (options.gridOnStart && options.gridOnStart.filter) {
        grid.dataSource['filter'] = options.gridOnStart.filter;
      }

      return grid;
    }
  }
});


