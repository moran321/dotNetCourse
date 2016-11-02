var ResultsService = (function () {
    function ResultsService(userSelectionService) {
        this.userSelectionService = userSelectionService;
        this.cheapestCarts = [];
        this.setCheapestCarts();
    }
    ResultsService.prototype.getCarts = function () {
        console.log('get carts');
        console.log(this.cheapestCarts);
        return this.cheapestCarts;
    };
    ResultsService.prototype.setCheapestCarts = function () {
        var carts = [];
        for (var _i = 0, _a = this.userSelectionService.storesToCompare; _i < _a.length; _i++) {
            var store = _a[_i];
            var cart = new ViewCart();
            cart.chainName = store.chainName;
            cart.store = store.address;
            for (var _b = 0, _c = this.userSelectionService.cartItems; _b < _c.length; _b++) {
                var item = _c[_b];
                var selected = new SelectedItem();
                selected.item = item.item;
                selected.quantity = item.quantity;
                selected.price = store.getPrice(item.item) * item.quantity;
                cart.addItem(selected);
            }
            cart.cartPrice = this.totalPrice(cart);
            carts.push(cart);
        }
        carts.sort(function (a, b) { return a.cartPrice - b.cartPrice; });
        this.cheapestCarts = carts.slice(0, 3);
    };
    ResultsService.prototype.totalPrice = function (cart) {
        var total = 0;
        for (var _i = 0, _a = cart.items; _i < _a.length; _i++) {
            var item = _a[_i];
            total += item.price;
        }
        return total;
    };
    return ResultsService;
}());
var StoreResultsCtrl = (function () {
    function StoreResultsCtrl(resultsService) {
        this.resultsService = resultsService;
    }
    StoreResultsCtrl.prototype.isCheapest = function () {
        if (this.resultsService.cheapestCarts[0] === this.cart) {
            return true;
        }
        return false;
    };
    return StoreResultsCtrl;
}());
