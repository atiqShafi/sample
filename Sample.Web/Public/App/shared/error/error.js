angular.module('app').config(function ($stateProvider) {

  $stateProvider.state('error-403', {
    isPublic: true,
    url: '/error/access-denied',
    templateUrl: 'public/app/shared/error/403.html'
  });

  $stateProvider.state('error-500', {
    isPublic: true,
    url: '/error/internal-server-error',
    templateUrl: 'public/app/shared/error/500.html'
  });

});






