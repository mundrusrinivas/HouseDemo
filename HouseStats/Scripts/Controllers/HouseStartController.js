var HouseStartController = function ($scope) {
    //$scope.init = function (houseinfo) {
    //    $scope.houseinfo = houseinfo;
    //}

    $scope.data = [
    { Name: 'Prakash', Age: 26, Number: 9800775544 },
    { Name: 'Sushil', Age: 27, Number: 9800774433 },
    { Name: 'Sagar', Age: 25, Number: 9800777787 }
    ];
    $scope.gridOptions = { data: 'data' };
    //$scope.gridOptions = { data: 'houseinfo.SourceData' };
}

HouseStartController.$inject = ['$scope'];