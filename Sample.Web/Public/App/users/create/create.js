angular.module('app').config(function ($stateProvider, templatesRoot) {
  $stateProvider.state('users.create', {
    url: '/create-user',
    templateUrl: templatesRoot + 'users/create/create.html',
    controller: function ($scope,$state, ctsForm, ctsNotification) {
      $scope.createUser = function (createUserForm) {
        $scope.isBusy = true;
        ctsForm.handle('users/create', {
          firstName: createUserForm.firstName,
          lastName: createUserForm.lastName,
          password: createUserForm.password,
          email: createUserForm.email
        }).then(function (result) {
          $scope.isBusy = false;
          ctsNotification.success(result.message);
          $state.go('users.list', { reload: true });

        }, function (err) {
          $scope.errors = err;
          $scope.isBusy = false;
        });
      }
    }
  });
});