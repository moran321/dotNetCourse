var StoreResultsCtrl = (function () {
    function StoreResultsCtrl(resultsService) {
        this.resultsService = resultsService;
    }
    StoreResultsCtrl.prototype.isCheapest = function () {
        if (this.resultsService.cheapestCarts[0] === this.cart) {
            return true;
        }
        return false;
    };
    StoreResultsCtrl.prototype.getItems = function () {
        return this.resultsService.getItems(this.cart);
    };
    StoreResultsCtrl.prototype.getPriceClass = function (item) {
        if (item.bestOrWorst === BestOrWorst.bestPrice) {
            return 'best-price';
        }
        if (item.bestOrWorst === BestOrWorst.worstPrice) {
            return "worst-price";
        }
        if (item.bestOrWorst === BestOrWorst.regularPrice) {
            return "regular-price";
        }
    };
    return StoreResultsCtrl;
}());
