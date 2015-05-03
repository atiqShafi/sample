angular.module('app').directive('ctsPassword', function (templatesRoot) {
  return {
    restrict: 'AE',
    templateUrl: templatesRoot + 'core/form/ctsPassword.html',
    require: 'ngModel',
    scope: {
      model: '=ngModel',
      title: '@',
      error: '@',
      placeholder: '@',
      value: '@'
    },
    link: function ($scope, element, attrs) {
      if ($scope.value) {
        $scope.model = $scope.value;
      }
      if (attrs.required) {
        $scope.isRequired = true;
      }

    }
  }
});
