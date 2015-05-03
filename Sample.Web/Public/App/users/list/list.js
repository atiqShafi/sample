angular.module('app').config(function($stateProvider, templatesRoot) {
  $stateProvider.state('users.list', {
    url: '/users',
    templateUrl: templatesRoot + 'users/list/list.html',
    controller: function ($scope,$state,ctsGrid) {
      $scope.usersGrid = ctsGrid.create({
        excelFileName: 'Users.xlsx',
        url: 'users/users-grid',
        schema: {
          model: {
            fields: {
              created: { type: "date" }
            }
          }
        },
        columns: [
          { field: "firstName", title: "First name ", filterable: { cell: { operator: "contains" } } },
          { field: "lastName", title: "Last name ", filterable: { cell: { operator: "contains" } } },
          { field: "email", title: "E-mail", filterable: { cell: { operator: "contains" } } },
          {
            field: "created",
            title: "Created",
            format: "{0:dd. MM. yyyy HH:mm}",
            filterable: false
          }
        ],
        sort: { field: "lastName", dir: "asc" },
        filterable: { mode: "row" }
      });
      $scope.selectUser = function (user) {
        $state.go('users.edit', { userId: user.id });
      }
    }
  });
});