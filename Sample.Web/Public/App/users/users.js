angular.module('app').config(function ($stateProvider, templatesRoot) {
  $stateProvider.state('users', {
    isPublic: true,
    authenticate: true,
    url: '/modul',
    templateUrl: templatesRoot + 'users/users.html',
    abstract: true
  });
});