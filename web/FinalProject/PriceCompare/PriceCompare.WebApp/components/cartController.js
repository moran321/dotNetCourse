var CartService = (function () {
    function CartService() {
        this.cart = new Cart();
    }
    return CartService;
}());
app.service("cartService", CartService);
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
//class CartCtrl {
//    public item: Item;
//    public buttonText: string;
//    public func: Function;
//    public isAddOrRemove: boolean; //true-add, false-remove
//    constructor(private cartService: CartService) {     }
//    addItem(item: Item) {
//        this.cartService.cart.addItem(item);
//    }
//    removeItem(item: Item) {
//        this.cartService.cart.addItem(item);
//    }
//    getCart() {
//        return this.cartService.cart;
//    }
//    isCartEmpty() {
//        if (this.cartService.cart.itemsInCart.length == 0) {
//            return true;
//        }
//        return false;
//    }
//    public onClick() {
//        this.func
//    }
//} 
//# sourceMappingURL=cartController.js.map