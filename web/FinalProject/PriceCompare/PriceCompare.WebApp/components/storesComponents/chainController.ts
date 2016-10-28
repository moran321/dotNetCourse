
class ChainsCtrl {
    chains: Chain[];
    selectedChain: Chain;
    selectedStore: Store;
    numOfStoresToCompare: number[];

    public constructor(private cartService: CartService) {

        var stores = [new Store("תל אביב")];
        var stores2 = [new Store("הרצליה פיתוח")];
        var stores3 = [new Store("תל אביב"), new Store("אשדוד")];

        var chains = [new Chain("מגה", stores),
            new Chain("חצי חינם", stores2),
            new Chain("רמי לוי", stores3)
        ];

        this.chains = chains;
        this.numOfStoresToCompare = [1,2];
    }

    public onStoreChanged(index: number) {
        if (index === 1) {
            this.cartService.cart.store = this.selectedStore;
        }
    }

    public addStore() {
        if (this.numOfStoresToCompare.length >= this.chains.length) {
            return;
        }
        this.numOfStoresToCompare.push(this.numOfStoresToCompare.length+1);
    }
}

class Chain {
    name: string;
    stores: Store[];

    constructor(name: string, stores: Store[]) {
        this.name = name;
        this.stores = stores;
    }
}



app.component("maStores", {
    templateUrl: "components/storesComponents/stores.html",
    controller: ChainsCtrl
});
