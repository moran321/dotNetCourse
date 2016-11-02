var app = angular
    .module("myApp", ["ui.bootstrap", "ngRoute"])
    .component("maHeader", {
    templateUrl: "components/headerComponents/header.html",
    controller: HeaderCtrl
})
    .component("maStores", {
    templateUrl: "components/storesComponents/stores.html",
    controller: StoresCtrl
})
    .component("maStorePicker", {
    templateUrl: "components/storesComponents/storePicker/store-picker.html",
    bindings: {
        number: "<",
    },
    controller: StorePickerCtrl
})
    .component("maItems", {
    templateUrl: "components/itemsComponents/items.html",
    controller: ItemsCtrl
})
    .component("maItemEntry", {
    templateUrl: "components/itemsComponents/itemsEntry/item-entry.html",
    bindings: {
        item: "=",
        buttonText: "@",
        onButtonClick: "&"
    },
    controller: ItemEntryCtrl
})
    .component("maResults", {
    templateUrl: "components/resultsComponent/results.html",
    controller: ResultsCtrl
})
    .component("maStoreResults", {
    templateUrl: "components/resultsComponent/storeResults.html",
    bindings: {
        cart: "="
    },
    controller: StoreResultsCtrl
})
    .service("storesService", StoresService)
    .service("userSelectionService", UserSelectionService)
    .service("resultsService", ResultsService)
    .service("dataService", DataService)
    .config(function ($routeProvider) {
    $routeProvider
        .when("/", {
        templateUrl: "home.html"
    })
        .when("/results", {
        templateUrl: "resultsPage.html"
    });
});
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
