function RegistreCtrl($scope, $http) {

    $scope.Urls = getModelForKeyURLS();
    $scope.model = getModel();

    //$scope.GetGender = function () {
        
    //    $http.get($scope.Urls.GetGender).success(function (data) {
    //        debugger;
    //        //alert(data);
    //        $scope.gender = data;
    //    }).
    //        error(function (data) {
    //            debugger;
    //            alert("NOPS!");
    //        });
    //}
    //$scope.gender=$scope.GetGender();

    $scope.Pass1;
    $scope.Pass2;
    $scope.email;

    $scope.CreatePlayer = function () {

        if ($scope.Pass1 == $scope.Pass2) {

            $scope.model.Email = $scope.email;
            $scope.model.Password = $scope.Pass1;
            alert($scope.model);

            $http.post($scope.Urls.CreatePlayer, $scope.model)

            .success(function (data) {
                debugger;
                alert("Usuari registrat");
            }).error(function (data) {
                alert("No s'ha pogut crear l'usuari");
            });

        } else {
            alert("Els passwords no coincideixen");
        }
        
        
    }
}