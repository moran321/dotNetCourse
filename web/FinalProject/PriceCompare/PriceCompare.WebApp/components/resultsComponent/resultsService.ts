class ResultsService {
    public cheapestCarts: ViewCart[];

    constructor(private userSelectionService: UserSelectionService) {
        this.cheapestCarts = [];
    }

    public setCheapestCarts() {
        var carts = [];
        for (let store of this.userSelectionService.storesToCompare) {
            var cart = new ViewCart();
            cart.chainName = store.chainName;
            cart.store = store.address;
            for (let item of this.userSelectionService.cartItems) {
                var selected = new SelectedItem();
                selected.item = angular.copy(item.item);
                selected.quantity = item.quantity;
                selected.price = store.getPrice(item.item) * item.quantity;
                cart.addItem(selected);
            }
            cart.cartPrice = this.totalPrice(cart);
            carts.push(cart);
        }
        carts.sort(function (a: ViewCart, b: ViewCart) { return a.cartPrice - b.cartPrice });
        this.cheapestCarts = carts.slice(0, 3);
    }

    private totalPrice(cart: ViewCart) {
        var total = 0;
        for (let item of cart.items) {
            total += item.price;
        }
        return total;

    }

    public getCarts(): ViewCart[] {
        return this.cheapestCarts;
    }


    public getItems(cart: ViewCart): SelectedItem[] {
        var index = this.cheapestCarts.indexOf(cart);
        return this.cheapestCarts[index].items;
    }


    public setItemsPriceRate() {
        for (let cart of this.cheapestCarts) {
            for (let item of cart.items) {
                this.bestItemPrice(item);
            }
        }
    }

    private bestItemPrice(item: SelectedItem) {


        var bestPrice = item.price;
        var worstPrice = item.price;
        for (let cart of this.cheapestCarts) {
            var p = cart.items.filter(x => (x.item.name === item.item.name))[0];
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

    }
}

