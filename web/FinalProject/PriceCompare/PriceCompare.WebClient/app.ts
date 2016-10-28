const app = angular.module("app", []);

//app.controller('chainController', function ($scope, $http) {
//    $scope.chains = "";
//    $http.get("api/Chains/getChains")
//        .success(function (response) {
//            $scope.chains = response.data;
//        }).error(function (response) {
//            console.log(response);
//        });
//});




interface IChainService {
    getChains();
}

class ChainService {

    constructor(private $http: ng.IHttpService) { }

    getChains() {

        return this.$http.get("api/chains")
            .then(data => { return data.data });

          
    }
}

app.service('chainService', ChainService);


class ChainsCtrl {

    chains: any[];

    constructor(chainService: IChainService) {
        chainService.getChains().then(data => { this.chains = data });
    }
}

app.controller('chainsCtrl', ChainsCtrl);