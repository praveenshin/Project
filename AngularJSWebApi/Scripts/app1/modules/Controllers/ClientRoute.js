//AngularJS module
var app = angular.module('RouteApp', []);

//AngularJS controller
app.controller('RouteController', function ($scope, Service) {



    $scope.getAll = function () {
        var promiseGet = Service.getAll(); //The MEthod Call from service
        alert('1');
        promiseGet.then(function (pl) { $scope.Routes = pl.data },
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
            $scope.route = p1.data;
           
        },
        function (errorp1) {
            alert(errorp1.statustext);
        }
        );
    }
   

   
    $scope.add = function (Route)
    {
       
        alert(Route.Source + ' ' + Route.Destination);
        var promisePost = Service.post(Route);
        promisePost.then(function (response) {
            alert(response + 'Added Successfuly');
        }, function (err) {
            alert(err);
        });
    }
    $scope.edit=function(ide,Route)
    {
            var promisePut = Service.put(ide,Route);
            promisePut.then(function (pl) {
                alert('Updated Successfuly');
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
           
            $scope.id1 = "";
            $scope.id2 = "";
            $scope.Route.Source = "";
            $scope.Route.Destination = "";
            $scope.Route.Distance = "";
            
        }
   

    
});
app.service('Service', function ($http) {



    //Create new record
    this.post = function (route)
    {
        alert(route.Source + 'in post ' + route.Destination);
        return $http.post("http://localhost:62541/api/ServerRoute", route);
        
    }
    //Get Single Records
    this.get = function (id) {
        return $http.get("http://localhost:62541/api/ServerRoute/" + id);
    }

    //Get All Employees
    this.getAll = function () {
        return $http.get("http://localhost:62541/api/ServerRoute");
    }


    //Update the Record
    this.put = function (id, Route) {
        var request = $http({
            method: "put",
            url: "http://localhost:62541/api/ServerRoute/" + id,
            data:Route
        });
        return request;
    }
    //Delete the Record
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "http://localhost:62541/api/ServerRoute/" + id
        });
        return request;
    }
});