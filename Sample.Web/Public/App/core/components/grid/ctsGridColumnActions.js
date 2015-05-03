angular.module('app').factory('ctsGridColumnActions', function () {
  return {
    reset: function (grid, gridOnStart) {
      if (gridOnStart && gridOnStart.hiddenColumns) {
        _.forEach(grid.columns, function (column) {
          var isVisible = _.includes(gridOnStart.hiddenColumns, column.field);
          if (isVisible) {
            grid.hideColumn(column.field);
          } else {
            grid.showColumn(column.field);
          }
        });
      }
      
      if (gridOnStart && gridOnStart.filter) {
        var ds = grid.dataSource;
        ds.filter([
          {
            'filters': gridOnStart.filter
          }
        ]);
      }

      return grid;
    },
    showAll: function (grid) {
      _.forEach(grid.columns, function (column) {
        grid.showColumn(column.field);
      });

      return grid;
    },
    removeAllFilters: function(grid) {
      var ds = grid.dataSource;
      ds.filter({});

      return grid;
    }
  };
});