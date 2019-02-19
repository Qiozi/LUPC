var globalVer = {
    webTitle: "Computer & Specifications"
}

var httpConfig = {
    url: '',
    method: 'get',
    data: {},
    scope: function ($http) {
        return $http;
    },
    successFn: function (fn) {
        return fn();
    },
    errorFn: function (fn) {
        if (angular.isFunction(fn))
            return fn();
    }
}

var httpErrorCode = [{
    code: '0001',
    txt: ""
}, {
    code: '',
    txt: ''
}];

angular.module('starter.services', [])
    .factory('fnService', function () {
        return function () {};
    })
    .factory('wuHttp', function ($http) {
        return function (config) {
            config.method = config.method || 'get';
            $http({
                method: config.method,
                url: config.url,
                data: config.method == 'post' ? config.data : {},
                params: config.method == 'get' ? config.data : {}
            }).then(function (response) {
                if (response.statusText === "OK") {
                    var data = response.data;
                    if (data.Success) {
                        if (angular.isFunction(config.successFn)) {
                            config.successFn(data);
                        }
                    } else {
                        if (data.ErrMsg === "001") {
                            // TODO
                        } else if (data.ToUrl !== '' && !angular.isUndefined(data.ToUrl) && data.ToUrl !== null) {
                            window.location.href = data.ToUrl;
                        } else {
                            // TODO
                        }
                    }
                } else {
                    util.alertError("http status is error.");
                }
            }, function (response) {
                util.alertError("数据请求出错。")
                if (angular.isFunction(config.errorFn))
                    config.errorFn(response.data);
            });
        }
    })
    .filter('htmlContent', ['$sce', function ($sce) {
        return function (input) {
            return $sce.trustAsHtml(input);
        }
    }])
/* .filter('total', function () {
     return function (x) {
         for (var i = x.PriceInfos.length - 1; i >= 0; i--) {
             if (x.Qty >= x.PriceInfos[i].Qty) {
                 return x.Qty * x.PriceInfos[i].Sold;
             }
         }
         return x.Sold * x.Qty;
     }
 })*/
;

var myApp = angular.module("myApp", ["ui.router", 'ngAnimate', 'ngMaterial', 'starter.services', 'angularLazyImg'])
//.animation('.fade', util.fade);

myApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $urlRouterProvider
            .when("", "/home/0")
            //.when("prodlist", "/prodlist")
            .otherwise("/home/0");

        $stateProvider
            .state("home", {
                url: "/home/:cid",
                templateUrl: "templates/home.html",
                controller: "homeCtrl",
                resolve: {

                }
            })
            .state("detail", {
                url: '/detail/:cid/:sku',
                templateUrl: 'templates/detail.html',
                controller: 'detailCtrl',
                resolve: {

                }
            })
            .state('404', {
                url: '/home',
                templateUrl: 'templates/404.html'
            });

        //$locationProvider.html5Mode(true).hashPrefix('!');
    })
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('blue')
            .accentPalette('blue');
    })
    .controller("headCtrl", function ($rootScope, $scope, wuHttp, $stateParams) {
        $rootScope.webTitle = "Computer & Specifications ";
        $rootScope.cates = [];
        wuHttp({
            url: '/data/Cate/',
            successFn: function (response) {
                if (response.Success) {
                    $rootScope.cates = response.Data;
                    if (!angular.isUndefined($stateParams.cid)) {
                        angular.forEach($rootScope.cates, function (value, key) {
                            if (value.Id == $rootScope.cid) {
                                $rootScope.webTitle = "Computer & Specifications & " + value.Title;
                                $rootScope.title = value.Title;
                            }
                        })
                    }
                }
            }
        });
    })
    .controller("homeCtrl", function ($scope, $rootScope, wuHttp, $state, $stateParams, $timeout, $mdSidenav, $mdMedia) {
        $rootScope.cid = $stateParams.cid;
        $scope.products = [];
        $scope.cid = $stateParams.cid;
        $scope.title = $scope.cid == 0 ? "New Product" : "";
        $rootScope.webTitle = globalVer.webTitle;

        $scope.screenIsSmall = $mdMedia('sm');

        $timeout(function () {
            angular.forEach($rootScope.cates, function (v, k) {
                if ($scope.cid == v.Id) {
                    $scope.title = v.Title;
                    $rootScope.webTitle = globalVer.webTitle + "& " + v.Title;
                    console.log(v.Title);
                }
            });

        }, 500);

        wuHttp({
            url: '/data/Product/',
            data: {
                cid: $scope.cid
            },
            successFn: function (response) {
                if (response.Success) {
                    $scope.products = response.Data;
                }
            }
        });


        $scope.toggleLeft = buildDelayedToggler('left');

        function debounce(func, wait, context) {
            var timer;

            return function debounced() {
                var context = $scope,
                    args = Array.prototype.slice.call(arguments);
                $timeout.cancel(timer);
                timer = $timeout(function () {
                    timer = undefined;
                    func.apply(context, args);
                }, wait || 10);
            };
        }

        function buildDelayedToggler(navID) {
            return debounce(function () {
                // Component lookup should always be available since we are not using `ng-if`
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        //  $log.debug("toggle " + navID + " is done");
                    });
            }, 200);
        }
    })
    .controller("detailCtrl", function ($scope, $rootScope, wuHttp, $state, $stateParams, $timeout, $mdSidenav) {
        $rootScope.cid = $stateParams.cid;
        $scope.sku = $stateParams.sku;
        $scope.description = "";
        $scope.product = {};
        $timeout(function () {
            angular.forEach($rootScope.cates, function (v, k) {
                if ($scope.cid == v.Id) {
                    $scope.title = v.Title;
                    $rootScope.webTitle = globalVer.webTitle + "& " + v.Title;
                    console.log(v.Title);
                }
            });

        }, 500);

        wuHttp({
            url: '/data/Cate/',
            successFn: function (response) {
                if (response.Success) {
                    $scope.cates = response.Data;
                    console.log(response.Data);
                }
            }
        });

        wuHttp({
            url: '/data/Product/Detail',
            data: {
                sku: $scope.sku
            },
            successFn: function (response) {
                if (response.Success) {
                    $scope.product = response.Data;
                    $rootScope.webTitle = $scope.product.ProduName;
                }
            }
        });
        $scope.toeBayWebsite = function () {
            if ($scope.product.eBayCode)
                window.location.href = "http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + $scope.product.eBayCode;
        }
        $scope.toLUWebsite = function () {
            window.location.href = "https://www.lucomputers.com/" + $scope.product.WebHref;
        }
        $scope.toggleLeft = buildDelayedToggler('left');

        function debounce(func, wait, context) {
            var timer;

            return function debounced() {
                var context = $scope,
                    args = Array.prototype.slice.call(arguments);
                $timeout.cancel(timer);
                timer = $timeout(function () {
                    timer = undefined;
                    func.apply(context, args);
                }, wait || 10);
            };
        }

        function buildDelayedToggler(navID) {
            return debounce(function () {
                // Component lookup should always be available since we are not using `ng-if`
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        //  $log.debug("toggle " + navID + " is done");
                    });
            }, 200);
        }
    });
/**/
myApp.run(function ($rootScope, $window, $location) {
    $rootScope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {

        });
});