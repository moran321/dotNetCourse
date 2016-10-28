class CartService {
  
    cart: Cart;

    constructor() {
        this.cart = new Cart();
    }
}
app.service("cartService", CartService);

 class Cart {
    itemsInCart: Item[];
    store: Store;

    constructor() {
        this.itemsInCart = [];
    }

    addItem(item: Item) {
        this.itemsInCart.push(item);

    }

    removeItem(item: Item) {
        var index = this.itemsInCart.indexOf(item);
        if (index != -1) {
            this.itemsInCart.splice(index, 1)
        }
    }
}


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