var ItemsCtrl = (function () {
    function ItemsCtrl(cartService) {
        this.cartService = cartService;
    }
    ItemsCtrl.prototype.isCartEmpty = function () {
        if (this.cartService.cart.itemsInCart.length == 0) {
            return true;
        }
        return false;
    };
    ItemsCtrl.prototype.getCart = function () {
        return this.cartService.cart;
    };
    ItemsCtrl.prototype.addItem = function (item) {
        this.cartService.cart.addItem(item);
        this.selectedText = '';
    };
    ItemsCtrl.prototype.removeItem = function (item) {
        this.cartService.cart.removeItem(item);
    };
    return ItemsCtrl;
}());
//# sourceMappingURL=items.js.map