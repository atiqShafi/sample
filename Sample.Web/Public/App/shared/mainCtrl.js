angular.module('app').controller('mainCtrl', function ($scope, $rootScope, $state, $http, $templateCache, ctsNotification,ctsAuthService, isDebugMode) {
  $scope.isDebugMode = isDebugMode;
  if ($rootScope.loggedUser) {
    $scope.loggedUser = $rootScope.loggedUser;
  }
 
  $scope.logout = function () {
    $http.post('logout').then(function() {
      $rootScope.loggedUser = null;
      $scope.loggedUser = null;
      $state.go('login', { reload: true });
      ctsNotification.success('You have been logged out');
    });
  }

  $scope.$on('loginSuccess', function (event,user) {
    $scope.loggedUser = user;
  });

});
