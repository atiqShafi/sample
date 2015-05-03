var app = angular.module('app', ['ui.router', 'angular-loading-bar', 'ncy-angular-breadcrumb', 'ngSanitize', 'ui.bootstrap', 'kendo.directives', 'ui.select', 'angularMoment', 'mgcrea.ngStrap.datepicker']);

app.constant('templatesRoot', 'Public/App/');

app.config(function ($urlRouterProvider, $modalProvider, $httpProvider, uiSelectConfig, $datepickerProvider) {
  // application settings
  $modalProvider.options.backdrop = 'static';
  uiSelectConfig.theme = 'bootstrap';

  // default route
  $urlRouterProvider.otherwise(function ($injector) {
    var $state = $injector.get('$state');
    var ctsAuthService = $injector.get('ctsAuthService');
    $state.go(ctsAuthService.getRedirectState());
  });

  angular.extend($datepickerProvider.defaults, { dateFormat: 'longDate', startWeek: 1, autoclose: true });
  if (!$httpProvider.defaults.headers.get) {
    $httpProvider.defaults.headers.get = {}; 
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Sat, 01 Jan 2000 00:00:00 GMT';

  }

  // error handling
  $httpProvider.interceptors.push(function ($q, $injector, $rootScope, $sce, ctsNotification, isDebugMode) {
    return {
      responseError: function (rejection) {
        if (rejection.config.nointercept) {
          return $q.reject(rejection);
        }

        if (rejection.status === 500) {
          if (isDebugMode) {
            var error = $injector.get('ctsDebugError');
            error(rejection.data);
          }
          else {
            $injector.get('$modalStack').dismissAll();
            $injector.get('$state').go('error-500');
          }
        }

        if (rejection.status === 404) {
          if (rejection.data.error) {
            ctsNotification.error(rejection.data.error);
          } else {
            ctsNotification.error(rejection.config.url + " not found");
          }
        }

        if (rejection.status === 403) {
          $injector.get('$modalStack').dismissAll();
          var state = $injector.get("ctsAuthService").getRedirectState();
          $injector.get('$state').go(state);
        }
        return $q.reject(rejection);
      }
    }
  });

});

app.run(function ($rootScope, $state, $stateParams, $templateCache, $location, ctsAuthService, amMoment,loggedUser,ctsNotification) {
  $rootScope.$state = $state;
  $rootScope.$stateParams = $stateParams;
  if (!$.isEmptyObject(loggedUser)) {
    $rootScope.loggedUser = loggedUser;
  }

  $rootScope.$on('cfpLoadingBar:started', function (event, data) {
    $('#progress-bar-overlay').show();
  });

  $rootScope.$on('cfpLoadingBar:completed', function (event, data) {
    $('#progress-bar-overlay').hide();
  });

  $rootScope.$on('$stateNotFound', function (event, toState, toParams, fromState, fromParams, error) {
    ctsNotification.error('Transition failed to state ' + toState.to);
  });

  $rootScope.$on("$stateChangeStart", function (event, toState) {

    if (toState.isPublic) {
      return;
    }
    if (!ctsAuthService.loggedUser()) {
      event.preventDefault();
      $state.go(ctsAuthService.getRedirectState());
    }

    if (toState.roles && !ctsAuthService.hasLoggedUserRole(toState.roles)) {
      event.preventDefault();
      $state.go(ctsAuthService.getRedirectState());
    }
  });

  amMoment.changeLocale('cs');
});

$.ajaxSetup({ cache: false });
$(document).ajaxError(function (event, jqxhr, settings) {
  if (jqxhr.status === 500) {
    var errorService = angular.element('*[ng-app]').injector().get("ctsDebugError");
    errorService(jqxhr.responseText);
  }

  if (jqxhr.status === 403) {
    var state = angular.element('*[ng-app]').injector().get("ctsAuthService").getRedirectState();
    angular.element('*[ng-app]').injector().get("$state").go(state);
  }


  if (jqxhr.status === 404) {
    toastr.error(settings.url + "not found");
  }
});
