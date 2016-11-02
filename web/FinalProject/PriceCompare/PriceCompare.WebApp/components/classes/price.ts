
class Price {
    item: Item;
    price: number;

    constructor(item: Item, price: number) {
        this.item = item;
        this.price = price;
    }

    public getItem() {
        return this.item;
    }
}
