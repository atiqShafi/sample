angular.module('app').directive('ctsInput', function (templatesRoot) {
  return {
    restrict : 'AE',
    templateUrl: templatesRoot + 'core/form/ctsInput.html',
    require: 'ngModel',
    scope: {
      model: '=ngModel',
      title: '@',
      error: '@',
      placeholder: '@',
      value: '@'
    },
    link: function ($scope, element, attrs) {
      if (attrs.required) {
        $scope.isRequired = true;
      }
      if ($scope.value) {
        $scope.model = $scope.value;
      }
    }
  }
});



