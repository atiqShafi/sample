angular.module('app').factory('ctsForm', function ($http, $q) {
  return {
    handle: function (url, formdata, file) {
      var serverErrors = {};
      var deffered = $q.defer();

      _.forEach(formdata, function (val, key) {
        if (typeof val === "undefined" || val === "null" || val === null) {
          delete formdata[key];
        }
      });

      if (file) {
        var fd = new FormData();
        fd.append(file.idname, file);
        _.forEach(formdata, function (val, key) {
          fd.append(key, val);
        });

        $http.post(url, fd, { ignoreLoadingBar: true, transformRequest: angular.identity, headers: { 'Content-Type': undefined } })
          .success(function (result) { deffered.resolve(result); })
          .error(function (result) { return onError(result, serverErrors, deffered) });

      } else {
        $http.post(url, formdata, { ignoreLoadingBar: true, })
          .success(function (result) { deffered.resolve(result); })
          .error(function (result) { return onError(result, serverErrors, deffered) });
      }

      return deffered.promise;
    }
  }

  function onError(r, serverErrors, deffered) {
    if (r.errors) {
      angular.forEach(r.errors, function (k, v) {
        serverErrors[v] = k;
      });
    }
    if (r.error) {
      serverErrors['errorMessage'] = r.error;
    }
    deffered.reject(serverErrors);
  }
});



