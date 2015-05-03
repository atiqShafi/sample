angular.module('app').factory('ctsAuthService', function ($rootScope) {

  function getState() {
      return 'login';
  }

  return {
    loggedUser: function () {
      return $rootScope.loggedUser;
    },
    hasLoggedUserRole: function (roles) {
      if (!$rootScope.loggedUser)
        return false;

      var hasUserRole = false;

      _.forEach(roles, function (role) {
        _.forEach($rootScope.loggedUser.roles, function (userRole) {
          if (role === userRole) {
            hasUserRole = true;
          }
        });
      });

      return hasUserRole;
    },
    getRedirectState: function () {
      if (!$rootScope.loggedUser)
        return getState();

      return "users.list";
    }
  };
});

