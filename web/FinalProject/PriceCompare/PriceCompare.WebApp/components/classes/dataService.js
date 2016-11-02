var DataService = (function () {
    function DataService($http, userSelectionService) {
        this.$http = $http;
        this.userSelectionService = userSelectionService;
        this.chains = [];
    }
    DataService.prototype.getChains = function () {
        return this.$http.get("http://localhost:7407/api/chains")
            .then(function (response) {
            console.log(response.data);
            return response.data;
        });
    };
    DataService.prototype.getItems = function () {
        return this.$http.get("http://localhost:7407/api/items?chainName=" +
            this.userSelectionService.storesToCompare[0].chainName
            + "storeName=" + this.userSelectionService.storesToCompare[0])
            .then(function (response) {
            console.log(response.data);
            return response.data;
        });
    };
    return DataService;
}());
