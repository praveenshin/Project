//AngularJS module
var app = angular.module('BusApp', []);

//AngularJS controller
app.controller('BusController', function ($scope, Service) {



    $scope.getAll = function () {
        var promiseGet = Service.getAll(); //The MEthod Call from service
        alert('1');
        promiseGet.then(function (pl) { $scope.Buses = pl.data },
              function (errorPl) {
                  alert(errorPl.statustext);
              });
    }


    $scope.get=function()
    {
        var id = $scope.id;
        alert(id);
        var promiseGet = Service.get(id);
        promiseGet.then(function (p1) {
            $scope.bus = p1.data;
           
        },
        function (errorp1) {
            alert(errorp1.statustext);
        }
        );
    }
   

   
    // Edit the record
    $scope.edit = function (ide, Bus)
    {
        var promisePut = Service.put(ide, Bus);
            promisePut.then(function (pl) {
                alert('Updated Successfuly');
                getAll();
            }, function (err) {
                alert(err.statustext);
            });
        }

        //Method to Delete
            $scope.remove = function ()
            {
                alert($scope.id2);
            var promiseDelete = Service.delete($scope.id2);
            promiseDelete.then(function (response) {
                alert('Deleted Successfuly');
            }, function (err) {
                alert(err.statustext);
            });
        }

        //Clear the Scopr models
        $scope.clear = function () {
            $scope.ide = "";
            $scope.id1 = "";
            $scope.id2 = "";
            $scope.Bus.bus_name = "";
            $scope.Bus.category_id = "";
            $scope.Bus.start_date = "";
            $scope.Bus.end_date = "";
            $scope.Bus.departure_time = "";
            $scope.Bus.route_id = "";
        }
   

        $scope.insert = function (Bus) {

            alert(Bus.bus_name + ' ' + Bus.category_id);
            var promisePost = Service.post(Bus);
            promisePost.then(function (response) {
                alert(response + 'Added Successfuly');
            }, function (err) {
                alert(err);
            });
        }
    
});
app.service('Service', function ($http) {



    //Create new record
    this.post = function (Bus)
    {
        alert(Bus.bus_name + 'in post ' + Bus.category_id);
        return $http.post("http://localhost:62541/api/ServerBus", Bus);
        
    }
    //Get Single Records
    this.get = function (id) {
        return $http.get("http://localhost:62541/api/ServerBus/" + id);
    }

    //Get All Employees
    this.getAll = function () {
        return $http.get("http://localhost:62541/api/ServerBus");
    }


    //Update the Record
    this.put = function (id, Bus) {
        var request = $http({
            method: "put",
            url: "http://localhost:62541/api/ServerBus/" + id,
            data: Bus
        });
        return request;
    }
    //Delete the Record
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "http://localhost:62541/api/ServerBus/" + id
        });
        return request;
    }
});