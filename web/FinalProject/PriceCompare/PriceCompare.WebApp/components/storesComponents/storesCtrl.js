var StoresCtrl = (function () {
    function StoresCtrl(storesService) {
        this.storesService = storesService;
    }
    StoresCtrl.prototype.addStore = function () {
        this.storesService.addStore();
    };
    return StoresCtrl;
}());
