var ItemEntryCtrl = (function () {
    function ItemEntryCtrl() {
    }
    ItemEntryCtrl.prototype.onClick = function () {
        this.onButtonClick({ item: this.item });
    };
    return ItemEntryCtrl;
}());
