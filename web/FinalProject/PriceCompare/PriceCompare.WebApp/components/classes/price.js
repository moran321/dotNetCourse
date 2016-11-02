var Price = (function () {
    function Price(item, price) {
        this.item = item;
        this.price = price;
    }
    Price.prototype.getItem = function () {
        return this.item;
    };
    return Price;
}());
