class UserSelectionService {
    cartItems: ViewItem[];
    storesToCompare: Store[];
    itemsList: ViewItem[];

    constructor() {
        this.cartItems = [];
        this.storesToCompare = [];
    }

    public resetCart() {

    }

    public numberOfItems() {
        return this.cartItems.length;
    }

    public getStoreItems(): ViewItem[] {
        return this.itemsList;
    }

    public getItems(prices: Price[]): ViewItem[] {
        var rtnItems = [];
        for (let price of prices) {
            rtnItems.push(new ViewItem(price.item,1));
        }
        return rtnItems;
    }

    public addStoresToCompare(store: Store) {
        this.storesToCompare.push(store);
        if (this.storesToCompare.length === 1) {
            this.itemsList = this.getItems(store.prices);
        }
    }

    private removeSelectedItemFromList(viewItem: ViewItem) {
        var index = this.itemsList.indexOf(viewItem);
        this.itemsList.splice(index, 1);
        
    }

    public changeStoreToCompare(store: Store, index: number) {
        if (index === 1) {
            this.itemsList = this.getItems(store.prices);
        }
        this.storesToCompare.splice(index-1, 1, store);
    }

    public addItem(viewItem: ViewItem) {
        this.cartItems.push(viewItem);
        this.removeSelectedItemFromList(viewItem);
    }

    public removeItem(viewItem: ViewItem) {
        var index = this.cartItems.indexOf(viewItem);
        if (index != -1) {
            this.cartItems.splice(index, 1);
            this.itemsList.push(viewItem);
        }
    }

}