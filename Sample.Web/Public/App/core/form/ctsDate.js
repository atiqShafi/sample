angular.module('app').directive('ctsDate', function (templatesRoot,$timeout) {
  return {
    restrict : 'AE',
    templateUrl: templatesRoot + 'core/form/ctsDate.html',
    scope: {
      ngModel: '=',
      title: '@',
      disableDates: '@',
      min: '=',
      max: '=',
      ngChange : '&',
      error: '@',
      value: '='
    },
    link: function (scope, element, attrs) {
      scope.updateModel = function (item) {
        $timeout(function() {
          scope.ngModel = item;
          scope.ngChange({ date: item });
        });
      }
      if (attrs.required) {
        scope.isRequired = true;
      }
      if (scope.value) {
        scope.model = scope.value;
      }
    }
  }
});



