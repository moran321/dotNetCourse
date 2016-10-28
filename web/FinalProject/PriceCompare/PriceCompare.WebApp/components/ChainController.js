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
    }
    ChainsCtrl.prototype.onStoreChanged = function () {
        this.cartService.cart.store = this.selectedStore;
    };
    ChainsCtrl.prototype.addStore = function () {
        var ul = document.getElementById("list");
        var children = ul.children.length;
        if (children >= this.chains.length) {
            return;
        }
        var sp = document.getElementById("storepicker");
        var cl = sp.cloneNode(true);
        var li = document.createElement("li");
        li.appendChild(cl);
        ul.appendChild(li);
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
