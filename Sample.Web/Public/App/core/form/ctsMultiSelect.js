angular.module('app').directive('ctsMultiSelect', function (templatesRoot) {
  return {
    restrict: 'AE',
    templateUrl: templatesRoot + 'core/form/ctsMultiSelect.html',
    replace: true,
    require: 'ngModel',
    scope: {
      model: '=ngModel',
      title: '@',
      error: '@',
      options: '=',
      onChange: '&',
      text: '=',
      placeholder: '@',
      groupBy: '@',    
    }, link: function ($scope, element, attrs) {
      if ($scope.value) {
        $scope.model = $scope.value;
      }
      if (attrs.required) {
        $scope.isRequired = true;
      }

      $scope.groupByFn = function (item) {
        if (item[attrs.groupBy]) {
          return item[attrs.groupBy];
        }
      };

    }
  }
});



