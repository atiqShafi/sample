angular.module('app').config(function ($stateProvider, templatesRoot) {
  $stateProvider.state('users.edit', {
    url: '/edit-user/{userId}',
    templateUrl: templatesRoot + 'users/edit/edit.html',
    resolve: {
      user: function ($http, $stateParams) {
        return $http.get('users/edit/' + $stateParams.userId);
      }
    },
    controller: function ($scope, $state, ctsForm, ctsNotification,ctsConfirm, user) {
      $scope.user = user.data;
      $scope.displayName = $scope.user.firstName + " " + $scope.user.lastName;

      $scope.editUser = function () {
        $scope.isBusy = true;
        var model = {
          firstName: $scope.user.firstName,
          lastName: $scope.user.lastName,
          email: $scope.user.email
        };
        ctsForm.handle('users/edit/' + $scope.user.userId, model).then(function (result) {
          ctsNotification.success(result.message);
          $state.go('users.list', { reload: true });
        }, function (err) { $scope.errors = err; $scope.isBusy = false; });
      }

      $scope.deleteUser = function () {
        var options = {
          content: 'Delete user  <strong>' + $scope.displayName + '</strong> ?',
          title: "Delete user",
          url: 'users/delete/' + $scope.user.userId,
          buttonText: "Delete",
          buttonBusyText: "Deleting"
        }
        ctsConfirm.show(options).then(function (result) {
          ctsNotification.success(result.message);
          $state.go('users.list', { reload: true });
        });
      }
    }
  });
});