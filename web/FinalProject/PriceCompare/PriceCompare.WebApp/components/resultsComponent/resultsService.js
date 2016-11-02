var ResultsService = (function () {
    function ResultsService(userSelectionService) {
        this.userSelectionService = userSelectionService;
        this.cheapestCarts = [];
    }
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
                selected.item = angular.copy(item.item);
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
    ResultsService.prototype.getCarts = function () {
        return this.cheapestCarts;
    };
    ResultsService.prototype.getItems = function (cart) {
        var index = this.cheapestCarts.indexOf(cart);
        return this.cheapestCarts[index].items;
    };
    ResultsService.prototype.setItemsPriceRate = function () {
        for (var _i = 0, _a = this.cheapestCarts; _i < _a.length; _i++) {
            var cart = _a[_i];
            for (var _b = 0, _c = cart.items; _b < _c.length; _b++) {
                var item = _c[_b];
                this.bestItemPrice(item);
            }
        }
    };
    ResultsService.prototype.bestItemPrice = function (item) {
        var bestPrice = item.price;
        var worstPrice = item.price;
        for (var _i = 0, _a = this.cheapestCarts; _i < _a.length; _i++) {
            var cart = _a[_i];
            var p = cart.items.filter(function (x) { return (x.item.name === item.item.name); })[0];
            //if (p == null) continue;
            if (p.price < item.price) {
                bestPrice = p.price;
                item.bestOrWorst = BestOrWorst.worstPrice;
            }
            else if (p.price > item.price) {
                worstPrice = p.price;
                item.bestOrWorst = BestOrWorst.bestPrice;
            }
        }
        if (item.price != bestPrice && item.price != worstPrice) {
            //    item.bestOrWorst = BestOrWorst.bestPrice;
            //} else if (item.price === worstPrice) {
            //    item.bestOrWorst = BestOrWorst.worstPrice;
            //} else {
            item.bestOrWorst = BestOrWorst.regularPrice;
        }
    };
    return ResultsService;
}());
