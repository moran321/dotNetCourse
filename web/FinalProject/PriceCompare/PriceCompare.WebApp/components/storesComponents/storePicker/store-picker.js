var StorePickerCtrl = (function () {
    function StorePickerCtrl(userSelectionService, storesService, dataService) {
        this.userSelectionService = userSelectionService;
        this.storesService = storesService;
        this.dataService = dataService;
        /**/
        this.chains = new Chains(dataService);
        /**/
    }
    StorePickerCtrl.prototype.getChains = function () {
        return this.chains.chains;
    };
    StorePickerCtrl.prototype.onStoreChanged = function (index) {
        if (this.selectedStore === null) {
            return;
        }
        this.selectedStore.chainName = this.selectedChain.name;
        if (this.selectedStore == null)
            return;
        if (index === 1) {
            this.userSelectionService.addStoresToCompare(this.selectedStore);
        }
        else {
            if (index > this.userSelectionService.storesToCompare.length) {
                this.userSelectionService.addStoresToCompare(this.selectedStore);
            }
            else {
                this.userSelectionService.changeStoreToCompare(this.selectedStore, index);
            }
        }
    };
    StorePickerCtrl.prototype.remove = function (index) {
        this.storesService.remove(index);
    };
    StorePickerCtrl.prototype.isLast = function (index) {
        if ((this.storesService.numOfStoresToCompare.length) === index && index > 2) {
            return true;
        }
        return false;
    };
    return StorePickerCtrl;
}());
