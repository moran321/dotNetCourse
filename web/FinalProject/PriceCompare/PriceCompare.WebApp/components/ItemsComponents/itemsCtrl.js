var ItemsCtrl = (function () {
    function ItemsCtrl(userSelectionService, dataService) {
        this.userSelectionService = userSelectionService;
        this.dataService = dataService;
    }
    ItemsCtrl.prototype.isCartEmpty = function () {
        if (this.userSelectionService.numberOfItems() == 0) {
            return true;
        }
        return false;
    };
    ItemsCtrl.prototype.getItemsInCart = function () {
        return this.userSelectionService.cartItems;
    };
    ItemsCtrl.prototype.getStoreItems = function () {
        // return  this.dataService.getItems();
        return this.userSelectionService.getStoreItems();
    };
    ItemsCtrl.prototype.addItem = function (viewItem) {
        this.userSelectionService.addItem(viewItem);
        this.selectedText = '';
    };
    ItemsCtrl.prototype.removeItem = function (viewItem) {
        this.userSelectionService.removeItem(viewItem);
    };
    return ItemsCtrl;
}());
