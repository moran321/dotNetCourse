var Chain = (function () {
    function Chain(name, stores) {
        this.name = name;
        this.stores = stores;
    }
    return Chain;
}());
var ChainDTO = (function () {
    function ChainDTO() {
    }
    return ChainDTO;
}());
var StoreDTO = (function () {
    function StoreDTO() {
    }
    return StoreDTO;
}());
var Chains = (function () {
    function Chains(dataService) {
        var _this = this;
        this.dataService = dataService;
        this.chains = [];
        this.datachains = [];
        this.dataService.getChains().then(function (data) {
            for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                var chain = data_1[_i];
                var stores = [];
                for (var _a = 0, _b = chain.stores; _a < _b.length; _a++) {
                    var storeDTO = _b[_a];
                    var store = new Store(storeDTO.adress);
                    stores.push(store);
                }
                var newChain = new Chain(chain.name, stores);
                _this.chains.push(newChain);
            }
        });
    }
    Chains.prototype.getChains = function () {
        return this.chains;
    };
    return Chains;
}());
