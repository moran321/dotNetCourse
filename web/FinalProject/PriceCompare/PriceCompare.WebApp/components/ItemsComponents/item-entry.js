var ItemEntryCtrl = (function () {
    function ItemEntryCtrl(cartService) {
        this.cartService = cartService;
    }
    return ItemEntryCtrl;
}());
//app.component("maItemEntry", {
//    templateUrl: "components/itemsComponents/item-entry.html",
//    bindings: {
//        item: "=",
//        buttonText: "@",
//        func: "&"
//    },
//    controller: ItemEntryCtrl
//});
