var UserSelectionService = (function () {
    function UserSelectionService() {
        this.cartItems = [];
        this.storesToCompare = [];
    }
    UserSelectionService.prototype.addItem = function (item) {
        this.cartItems.push(item);
    };
    UserSelectionService.prototype.removeItem = function (item) {
        var index = this.cartItems.indexOf(item);
        if (index != -1) {
            this.cartItems.splice(index, 1);
        }
    };
    return UserSelectionService;
}());
