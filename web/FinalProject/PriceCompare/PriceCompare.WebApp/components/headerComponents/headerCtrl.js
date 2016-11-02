var HeaderCtrl = (function () {
    function HeaderCtrl(userSelectionService) {
        this.userSelectionService = userSelectionService;
    }
    HeaderCtrl.prototype.numberOfItems = function () {
        return this.userSelectionService.numberOfItems();
    };
    return HeaderCtrl;
}());
