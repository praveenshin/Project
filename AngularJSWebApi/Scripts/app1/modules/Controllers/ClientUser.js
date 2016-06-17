//AngularJS module
var app = angular.module('UserApp', []);

//AngularJS controller
app.controller('UserController', function ($scope, Service) {



    $scope.getAll = function () {
        var promiseGet = Service.getAll(); //The MEthod Call from service
        alert('1');
        promiseGet.then(function (pl) { $scope.Users = pl.data },
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
            $scope.user = p1.data;
           
        },
        function (errorp1) {
            alert(errorp1.statustext);
        }
        );
    }
   

   
    $scope.add = function (User)
    {
       
        alert(User.Name);
        var promisePost = Service.post(User);
        promisePost.then(function (response) {
            alert(response + 'Added Successfuly');
        }, function (err) {
            alert(err);
        });
    }
    // Edit the record
    $scope.edit = function (ide, User)
    {
        var promisePut = Service.put(ide, User);
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

            $scope.id1 = "";
            $scope.id2 = "";
            $scope.User.Name = "";
            $scope.User.date_of_birth = "";
            $scope.User.email_id = "";
            $scope.User.mobile_no = "";
            $scope.User.Password = "";
            $scope.User.Address = "";
        }
   

    
});
app.service('Service', function ($http) {



    //Create new record
    this.post = function (user)
    {
        alert(user.Name + 'in post '+user.Phone+' '+user.Dob);
        return $http.post("http://localhost:62541/api/ServerUser",user);
        
    }
    //Get Single Records
    this.get = function (id) {
        return $http.get("http://localhost:62541/api/ServerUser/" + id);
    }

    //Get All Employees
    this.getAll = function () {
        return $http.get("http://localhost:62541/api/ServerUser");
    }


    //Update the Record
    this.put = function (id, user) {
        var request = $http({
            method: "put",
            url: "http://localhost:62541/api/ServerUser/" + id,
            data: user
        });
        return request;
    }
    //Delete the Record
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "http://localhost:62541/api/ServerUser/" + id
        });
        return request;
    }
});