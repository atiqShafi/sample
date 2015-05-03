angular.module('app').directive('ctsCheckbox', function (templatesRoot,$timeout) {
  return {
    restrict : 'AE',
    templateUrl: templatesRoot + 'core/form/ctsCheckbox.html',
    require: 'ngModel',
    scope: {
      model: '=ngModel',
      ngChange: '&',
      title: '@',
      error: '@',
      value: '='
    },
    link: function (scope) {
      if (scope.value) {
        scope.model = scope.value;
      }
      scope.updateModel = function (item) {
        $timeout(function () {
          scope.ngModel = item;
          scope.ngChange({ date: item });
        });
      }
    }
  }
});



