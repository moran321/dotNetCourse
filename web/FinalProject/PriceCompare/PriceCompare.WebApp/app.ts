const app = angular.module("myApp", ["ui.bootstrap"]);


//app.controller('chainController', function ($scope, $http) {
//    $scope.chains = "";
//    $http.get("api/chains/getAll")
//        .success(function (result){
//            $scope.chains = result.data;
//        }).error(function (result) {
//            console.log(result);
//    });
//});




//interface IChainService {
//    getChains();
//}

//class ChainService {

//    constructor(private $http: ng.IHttpService) { }

//    getChains(): ng.IPromise<any[]> {
//        return this.$http.get("api/Chains/getChains")
//            .then(data => { return data.data });
//    }
//}

//app.service('chainService', ChainService);


//class ChainsCtrl {

//    chains: any[];

//    constructor(chainService: IChainService) {
//        chainService.getChains().then(data => { this.chains = data }, error => { });
//    }
//}

//app.controller('chainsCtrl', ChainsCtrl);