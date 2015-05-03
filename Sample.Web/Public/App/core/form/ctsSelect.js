angular.module('app').directive('ctsSelect', function (templatesRoot) {
  return {
    restrict: 'AE',
    templateUrl: templatesRoot + 'core/form/ctsSelect.html',
    replace: true,
    require: 'ngModel',
    scope: {
      model: '=ngModel',
      title: '@',
      error: '@',
      options: '=',
      text: '=',
      placeholder: '@',
      groupBy: '@',
      onChange: '&'
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



