

class ItemEntryCtrl {

    constructor(private cartService: CartService) {

    }
}

app.component("maItemEntry", {
    templateUrl: "components/itemsComponents/item-entry.html",
    bindings: {
        item: "=",
        buttonText: "@",
        func: "&"
    },
    controller: ItemEntryCtrl
});
