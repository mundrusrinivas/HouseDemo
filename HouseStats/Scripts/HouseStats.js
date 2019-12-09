var HouseStats = angular.module('HouseStats', ['ngGrid']);
HouseStats.controller('HouseStartController', function ($scope) {
    $scope.init = function (houseinfo) {
        $scope.houseinfo = houseinfo;
    }
    $scope.gridOptions =  { data: 'houseinfo.SourceData' };
    $scope.sortDistance = { data: 'houseinfo.SortedHouseByDistance' };
    $scope.sortRooms = { data: 'houseinfo.SortedHouseByRooms' };
    $scope.sortStreet = { data: 'houseinfo.SortedHouseByStreet' };
    $scope.nearestHome = { data: 'houseinfo.NearestHouse' };
    //$scope.gridOptions = { data: 'houseinfo.SourceData' };
}
);

//HouseStats.controller('HouseStartController', function ($scope) {
//    $scope.init = function (houseinfo) {
//        $scope.houseinfo = houseinfo;
//    }

//    //$scope.data = [
//    //{ Name: 'Prakash', Age: 26, Number: 9800775544 },
//    //{ Name: 'Sushil', Age: 27, Number: 9800774433 },
//    //{ Name: 'Sagar', Age: 25, Number: 9800777787 }
//    //];
//    $scope.gridOptions = { data: 'houseinfo.SourceData' };
//    //$scope.gridOptions = { data: 'houseinfo.SourceData' };
//}
//);

//HouseStats.controller('HouseStartController', HouseStartController);

//var configFunction = function ($routeProvider) {
//    $routeProvider.
//        when('/HouseData', {
//            templateUrl: 'House/GetHouseData'
//        });
//}
//configFunction.$inject = ['$routeProvider'];
//HouseStats.config(configFunction);