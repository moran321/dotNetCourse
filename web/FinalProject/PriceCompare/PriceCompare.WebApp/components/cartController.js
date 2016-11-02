var CartService = (function () {
    function CartService() {
        this.cart = new Cart();
        this.comparedTo = [];
    }
    return CartService;
}());
var Cart = (function () {
    function Cart() {
        this.itemsInCart = [];
    }
    Cart.prototype.addItem = function (item) {
        this.itemsInCart.push(item);
    };
    Cart.prototype.removeItem = function (item) {
        var index = this.itemsInCart.indexOf(item);
        if (index != -1) {
            this.itemsInCart.splice(index, 1);
        }
    };
    return Cart;
}());
//# sourceMappingURL=cartController.js.map