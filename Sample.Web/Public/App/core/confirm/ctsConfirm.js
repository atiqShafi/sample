angular.module('app').service('ctsConfirm', function ($modal, $rootScope, $http, $q, templatesRoot, ctsNotification) {

  var scope = $rootScope.$new();
  var deferred;
  var confirmModal;

  confirm.show = function (options) {
    scope.title = options.title;
    scope.content = options.content;
    scope.buttonText = options.buttonText;
    scope.buttonBusyText = options.buttonBusyText;
    scope.url = options.url;
    scope.urlparams = options.urlparams;
    confirmModal = $modal.open({
      templateUrl: templatesRoot + "core/confirm/ctsConfirm.html", scope: scope,
    });
    deferred = $q.defer();
    return deferred.promise;
  }

  scope.ok = function () {

    if (scope.buttonBusyText && scope.url)
      scope.isBusy = true;

    if (scope.url) {
      confirmModal.close();
      $http.post(scope.url, scope.urlparams)
		    .success(function (result) {
		      confirmModal.close();
		      scope.isBusy = false;
		      deferred.resolve(result);
		    })
		    .error(function (r) {
        console.log(r);
		      scope.isBusy = false;
		      if (r.error) {
		        ctsNotification.error(r.error);
		      } else {
		        deferred.reject();
		      }
		    });
    } else {
      confirmModal.close();
      deferred.resolve(res);
    }
    return deferred.promise;
  }

  return confirm;
});

