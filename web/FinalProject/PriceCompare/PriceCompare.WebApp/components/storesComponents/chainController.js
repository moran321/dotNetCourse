var ChainsCtrl = (function () {
    function ChainsCtrl(cartService) {
        this.cartService = cartService;
        var stores = [new Store("תל אביב")];
        var stores2 = [new Store("הרצליה פיתוח")];
        var stores3 = [new Store("תל אביב"), new Store("אשדוד")];
        var chains = [new Chain("מגה", stores),
            new Chain("חצי חינם", stores2),
            new Chain("רמי לוי", stores3)
        ];
        this.chains = chains;
        this.numOfStoresToCompare = [1, 2];
    }
    ChainsCtrl.prototype.onStoreChanged = function (index) {
        if (index === 1) {
            this.cartService.cart.store = this.selectedStore;
        }
    };
    ChainsCtrl.prototype.addStore = function () {
        if (this.numOfStoresToCompare.length >= this.chains.length) {
            return;
        }
        this.numOfStoresToCompare.push(this.numOfStoresToCompare.length + 1);
    };
    return ChainsCtrl;
}());
var Chain = (function () {
    function Chain(name, stores) {
        this.name = name;
        this.stores = stores;
    }
    return Chain;
}());
