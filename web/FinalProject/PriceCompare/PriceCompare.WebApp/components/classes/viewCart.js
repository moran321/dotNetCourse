var ViewCart = (function () {
    function ViewCart() {
        this.items = [];
    }
    ViewCart.prototype.getTotalPrice = function () {
    };
    ViewCart.prototype.addItem = function (item) {
        this.items.push(item);
    };
    return ViewCart;
}());
