var Item = (function () {
    function Item(name, price, description) {
        this.Name = name;
        this.Price = price;
        this.Description = description;
        this.Pic = "https://www.iaap-hq.org/global_graphics/default-store-350x350.jpg";
    }
    return Item;
}());
var ItemsCtrl = (function () {
    function ItemsCtrl(cartService) {
        this.cartService = cartService;
    }
    return ItemsCtrl;
}());
app.component("maItems", {
    templateUrl: "components/itemsComponents/items.html",
    controller: ItemsCtrl
});
//# sourceMappingURL=items.js.map