
angular.module('app').config(function ($stateProvider) {
  $stateProvider.state('login', {
    isPublic: true,
    url: '/login',
    templateUrl: 'public/app/shared/login/login.html',
    controller: function ($scope, $rootScope, $state, ctsForm, ctsNotification) {
      $scope.loginForm = {};
      $scope.loginForm.password = "test";

      $scope.login = function (loginForm) {
        $scope.isBusy = true;
        ctsForm.handle('login', { email: loginForm.email, password: loginForm.password }).then(function (result) {
          $rootScope.loggedUser = result.data;
          $rootScope.$broadcast('loginSuccess', result.data);
          $state.go('users.list', { reload: true });
          ctsNotification.success("You have been logged on");
        }, function (err) { $scope.errors = err; $scope.isBusy = false; });
      }
    }
  });
});





