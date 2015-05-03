angular.module('app').service('ctsDebugError', function ($modal, $rootScope, $sce, templatesRoot) {
  return function (error) {
    var scope = $rootScope.$new();
    scope.content = $sce.trustAsHtml(error);
    $modal.open({
      templateUrl: templatesRoot + "core/debugError/ctsDebugError.html",
      scope: scope,
      backdrop: true,
      size: "lg",
      windowClass: 'error-modal'
    });
  };
});
