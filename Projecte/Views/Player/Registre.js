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

    $scope.CreatePlayer = function () {
        
        $http.post($scope.Urls.CreatePlayer, $scope.model)
        
            .success(function (data) {
                alert("Usuari registrat");
            }).error(function(data){
                alert("No s'ha pogut crear l'usuari");
            });
    }
}