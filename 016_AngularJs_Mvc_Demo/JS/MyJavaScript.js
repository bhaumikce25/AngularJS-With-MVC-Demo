var ModuleCRUD = angular.module("moduleCrud", []);
var baseAddress = '/User/';
var url = "";
var currentUser = null;

ModuleCRUD.factory('userFactory', function ($http) {
    return {
        getUsersList: function () {
            url = baseAddress + "GetUserList";
            return $http.get(url);
        },
        getUser: function (prmUser) {
            url = baseAddress + "GetUser/" + prmUser.UserID;
            return $http.get(url);
        },
        addUser: function (prmUser) {
            url = baseAddress + "AddUser";
            return $http.post(url, prmUser);
        },
        updateUser: function (prmUser) {
            url = baseAddress + "UpdateUser/" + prmUser.UserID;
            return $http.put(url, prmUser);
        },
        deleteUser: function (prmUser) {
            url = baseAddress + "DeleteUser/" + prmUser.UserID;
            return $http.delete(url);
        }
        
    };
});

ModuleCRUD.controller("crudCtrl", function PostController($scope, userFactory) {
    $scope.usersMaster = [];
    $scope.users = [];
    $scope.userMaster = null;
    $scope.user = {};
    $scope.userInline = {};
    $scope.searchString = "";
    $scope.editMode = false;
    $scope.addMode = false;
    
    $scope.orderByMe = function (x) {
        $scope.orderKey = x;
        $scope.reverse = !$scope.reverse;

        $scope.userMaster = null;
        $scope.user = {};
        $scope.userInline = {};
    }

    //default call get all Users
    $scope.getAllUser = function () {

        userFactory.getUsersList().success(function (data) {
            $scope.usersMaster = data;
            $scope.users = angular.copy($scope.usersMaster);
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading users! " + data.ExceptionMessage;
        });
    };

    //add User call from model(addUserShow)
    $scope.add = function () {
        currentUser = this.user;
        if (currentUser != null && currentUser.UName != null && currentUser.FName && currentUser.LName) {
            userFactory.addUser(currentUser).success(function (data) {
                currentUser.addMode = false;
                currentUser.UserID = data;
                $scope.usersMaster.push(currentUser);
                $scope.users = angular.copy($scope.usersMaster);
                //$scope.users.push(currentUser);

                //reset form
                $scope.userMaster = null;
                $scope.user = {};
                // $scope.adduserform.$setPristine(); //for form reset

                $('#userAddEditModal').modal('hide');
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding user! " + data.ExceptionMessage;
            });
        }
    };

    //update user call from model(editUserShow)
    $scope.update = function () {

        currentUser = $scope.user;
        userFactory.updateUser(currentUser).success(function (data) {

            for (i = 0 ; i < $scope.users.length ; i++) {
                if ($scope.users[i].UserID == currentUser.UserID) {

                    angular.copy(currentUser, $scope.users[i]);
                }
            }

            $scope.usersMaster = angular.copy($scope.users);
            $scope.userMaster = angular.copy($scope.user);

            $scope.userMaster = null;
            $scope.user = {};
            $scope.userInline = {};

            currentUser.editMode = false;

            $('#userAddEditModal').modal('hide');

        }).error(function (data) {
            $scope.error = "An Error has occured while Updating user! " + data.ExceptionMessage;
        });
    };

    // delete User call from model(confirmDialouge)
    $scope.delete = function () {
        currentUser = $scope.user;
        userFactory.deleteUser(currentUser).success(function (data) {
            $('#userDeleteConfirmationModel').modal('hide');
            
            for (i = 0 ; i < $scope.users.length ; i++) {
                if ($scope.users[i].UserID == currentUser.UserID) {
                    $scope.usersMaster.splice(i, 1);
                }
            }

            $scope.users = angular.copy($scope.usersMaster);
            //$scope.users.splice(index, 1);
            
        }).error(function (data) {
            $scope.error = "An Error has occured while Deleting user! " + data.ExceptionMessage;
            $('#confirmModal').modal('hide');
        });
    };

    // cancel call from model
    $scope.cancel = function () {

        $scope.users = angular.copy($scope.usersMaster);
        $scope.userMaster = null;
        $scope.user = {};
        $scope.userInline = {};

        $('#userAddEditModal').modal('hide');
        $('#userDeleteConfirmationModel').modal('hide');
    }

    $scope.refresh = function () {

        $scope.searchString = "";
        $scope.myOrder = "UserID";
    }

    //Model popup events
    //get user
    $scope.showUserViewModel = function (prmUser) {
        $scope.userInline = {};
        $scope.userMaster = prmUser;
        $scope.user = angular.copy($scope.userMaster);
        $('#userViewModal').modal('show');
    };

    //add user
    $scope.showUserAddModel = function () {
        $scope.userInline = {};
        $scope.userMaster = null;
        $scope.user = null;
        $scope.editMode = false;
        $scope.addMode = true;
        $('#userAddEditModal').modal('show');
    };

    //edit user
    $scope.showUserEditModel = function (prmUser) {
        $scope.userInline = {};
        $scope.userMaster = prmUser;
        $scope.user = angular.copy($scope.userMaster);
        $scope.addMode = false;
        $scope.editMode = true;
        $('#userAddEditModal').modal('show');
    };

    //delete user
    $scope.showConfirmDialog = function (prmUser) {
        $scope.userInline = {};
        $scope.userMaster = prmUser;
        $scope.user = angular.copy($scope.userMaster);
        $('#userDeleteConfirmationModel').modal('show');
    };

    // initialize your users data
    $scope.getAllUser();

    $scope.getTemplate = function (prmUser) {

        if (prmUser.UserID == $scope.userInline.UserID) {
            return 'edit';
        }
        else return 'display';
    };

    $scope.inlineEditUser = function (prmUser) {
        $scope.userMaster = prmUser;
        $scope.user = angular.copy($scope.userMaster);
        $scope.userInline = angular.copy($scope.userMaster);
        $scope.addMode = false;
        $scope.editMode = false;

    };

    $scope.reset = function () {
        $scope.userMaster = null;
        $scope.user = {};
        $scope.userInline = {};
    };

});

ModuleCRUD.directive('myFocus', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.$watch(attr.myFocus, function (n) {

                if (n != 0 && n) {
                    element[0].focus();
                    scope[attr.myFocus] = false;
                }
            });
        }
    };
});

//-------------------------------------------------------------------------------------------

angular.module("CombineModule", ["moduleCrud"]);
