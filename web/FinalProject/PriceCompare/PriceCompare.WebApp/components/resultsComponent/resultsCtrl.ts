class ResultsCtrl {
    constructor(private resultsService: ResultsService) {
        this.resultsService.setCheapestCarts();
        this.resultsService.setItemsPriceRate()
    }
    public getCarts() {

        var carts = this.resultsService.getCarts();
        if (carts.length > 2) {
            var orderedCarts = [carts[1], carts[0], carts[2]];

            return orderedCarts;
        }
        return carts;
    }

    

}