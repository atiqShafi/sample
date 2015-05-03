angular.module('app').directive('ctsHasRole', function (ctsAuthService) {
  return {
    restrict : 'AE',
    link: function (scope, element, attrs) {
      if (_.isEmpty(attrs.ctsHasRole))
        element.addClass('hidden');

      var roles = attrs.ctsHasRole.split(',');
      if (!ctsAuthService.hasLoggedUserRole(roles))
        element.addClass('hidden');
    }
  }
});



