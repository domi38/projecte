angular.module('myHttpInterceptor', [])
/* inspired by: http://stackoverflow.com/questions/11956827/angularjs-intercept-all-http-json-responses */
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('wHttpInterceptor');
    })
    .factory('wHttpInterceptor', function ($q) {
        return {
            response: function (response) {
                // do something on success
                debugger;
                if (response.headers()['content-type'] === "application/json; charset=utf-8") {
                    // Validate response if not ok reject
                    var data = revive_dates(response); // assumes this function is available

                    if (!data)
                        return $q.reject(response);
                }
                return response;
            },
            responseError: function (response) {
                // do something on error
                debugger;
                return $q.reject(response);
            }
        }
    }
        );

function revive_dates(obj) {
    for (var p in obj) {
        if (obj.hasOwnProperty(p)) {
            var value = obj[p];
            if (typeof value === 'string') {
                a = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
                if (a) {
                    value = new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4], +a[5], +a[6]));
                }
            }
            else {
                value = revive_dates(value);
            }
            obj[p] = value;
        }
    }
    return obj;
};


// Your app's root module...
angular.module('MyModule', [], function ($httpProvider) {
    // Use x-www-form-urlencoded Content-Type
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

    /**
     * The workhorse; converts an object to x-www-form-urlencoded serialization.
     * @param {Object} obj
     * @return {String}
     */
    var param = function (obj) {
        var query = '', name, value, fullSubName, subName, subValue, innerObj, i;

        for (name in obj) {
            value = obj[name];

            if (value instanceof Array) {
                for (i = 0; i < value.length; ++i) {
                    subValue = value[i];
                    fullSubName = name + '[' + i + ']';
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + '&';
                }
            }
            else if (value instanceof Object) {
                for (subName in value) {
                    subValue = value[subName];
                    fullSubName = name + '[' + subName + ']';
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + '&';
                }
            }
            else if (value !== undefined && value !== null)
                query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
        }

        return query.length ? query.substr(0, query.length - 1) : query;
    };

    // Override $http service's default transformRequest
    $httpProvider.defaults.transformRequest = [function (data) {
        return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
    }];
});

angular.module('joc', ['MyModule'])
;






function ajax_POST(URL, js_object, success, error) {
    debugger;
    var config = {
        url: URL,
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(js_object),
        success: revive_dates(success)
    };
    if (error) { config["error"] = error; }
    else { }
    $.ajax(config);
};