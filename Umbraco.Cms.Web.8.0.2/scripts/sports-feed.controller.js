(function () {
    "use strict";
    function Controller($http) {
        var vm = this;
        var url = 'https://sit-sportsbook-service.totalcoding-test1.com/api/sportevent/GetUpcomingEvents/';
        vm.feeds = [];

        function getFeed() {
            $http({
                method: 'GET',
                url: url
            }).then(function successCallback(response) {
                vm.feeds = response.data;
               
            }, function errorCallback(response) {
                //console.error(response.data);
                return false;
            });
        }
        getFeed();
    }
    app.controller("SportsFeedController", Controller);
})();