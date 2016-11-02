var StoresService = (function () {
    function StoresService() {
        this.numOfStoresToCompare = [1, 2];
    }
    StoresService.prototype.addStore = function () {
        //if (this.numOfStoresToCompare.length >= this.chains.length) {
        //    return;
        //}
        this.numOfStoresToCompare.push(this.numOfStoresToCompare.length + 1);
    };
    StoresService.prototype.remove = function (index) {
        this.numOfStoresToCompare.splice(index - 1, 1);
    };
    return StoresService;
}());
