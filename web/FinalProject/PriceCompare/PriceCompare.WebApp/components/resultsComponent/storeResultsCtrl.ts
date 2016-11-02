
class StoreResultsCtrl {
    public cart: ViewCart;

    constructor(private resultsService: ResultsService) {
        
    }

    public isCheapest() {
        if (this.resultsService.cheapestCarts[0] === this.cart) {
            return true;
        }
        return false;
    }

    public getItems() {
        return this.resultsService.getItems(this.cart);
    }

    public getPriceClass(item: SelectedItem) {
        if (item.bestOrWorst === BestOrWorst.bestPrice) {
            return 'best-price';
        }

        if (item.bestOrWorst === BestOrWorst.worstPrice) {
            return "worst-price";
        }

        if (item.bestOrWorst === BestOrWorst.regularPrice) {
            return "regular-price";
        }
    }
    
}
