var app = angular.module("app", []);
var ChainService = (function () {
    function ChainService($http) {
        this.$http = $http;
    }
    ChainService.prototype.getChains = function () {
        return this.$http.get("api/chains")
            .then(function (data) { return data.data; });
    };
    return ChainService;
}());
app.service('chainService', ChainService);
var ChainsCtrl = (function () {
    function ChainsCtrl(chainService) {
        var _this = this;
        chainService.getChains().then(function (data) { _this.chains = data; });
    }
    return ChainsCtrl;
}());
app.controller('chainsCtrl', ChainsCtrl);
//# sourceMappingURL=app.js.map