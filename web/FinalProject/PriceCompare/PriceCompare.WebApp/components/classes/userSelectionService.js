var UserSelectionService = (function () {
    function UserSelectionService() {
        this.cartItems = [];
        this.storesToCompare = [];
    }
    UserSelectionService.prototype.resetCart = function () {
    };
    UserSelectionService.prototype.numberOfItems = function () {
        return this.cartItems.length;
    };
    UserSelectionService.prototype.getStoreItems = function () {
        return this.itemsList;
    };
    UserSelectionService.prototype.getItems = function (prices) {
        var rtnItems = [];
        for (var _i = 0, prices_1 = prices; _i < prices_1.length; _i++) {
            var price = prices_1[_i];
            rtnItems.push(new ViewItem(price.item, 1));
        }
        return rtnItems;
    };
    UserSelectionService.prototype.addStoresToCompare = function (store) {
        this.storesToCompare.push(store);
        if (this.storesToCompare.length === 1) {
            this.itemsList = this.getItems(store.prices);
        }
    };
    UserSelectionService.prototype.removeSelectedItemFromList = function (viewItem) {
        var index = this.itemsList.indexOf(viewItem);
        this.itemsList.splice(index, 1);
    };
    UserSelectionService.prototype.changeStoreToCompare = function (store, index) {
        if (index === 1) {
            this.itemsList = this.getItems(store.prices);
        }
        this.storesToCompare.splice(index - 1, 1, store);
    };
    UserSelectionService.prototype.addItem = function (viewItem) {
        this.cartItems.push(viewItem);
        this.removeSelectedItemFromList(viewItem);
    };
    UserSelectionService.prototype.removeItem = function (viewItem) {
        var index = this.cartItems.indexOf(viewItem);
        if (index != -1) {
            this.cartItems.splice(index, 1);
            this.itemsList.push(viewItem);
        }
    };
    return UserSelectionService;
}());
