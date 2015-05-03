angular.module('app').directive('ctsServerErrors', function (templatesRoot) {
  return {
    restrict: 'AE',
    scope: {
      errors: '='
    },
    templateUrl: templatesRoot + 'core/serverError/ctsServerErrors.html'
  };
});



