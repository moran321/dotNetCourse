var ResultsCtrl = (function () {
    function ResultsCtrl(resultsService) {
        this.resultsService = resultsService;
        this.resultsService.setCheapestCarts();
        this.resultsService.setItemsPriceRate();
    }
    ResultsCtrl.prototype.getCarts = function () {
        var carts = this.resultsService.getCarts();
        if (carts.length > 2) {
            var orderedCarts = [carts[1], carts[0], carts[2]];
            return orderedCarts;
        }
        return carts;
    };
    return ResultsCtrl;
}());
