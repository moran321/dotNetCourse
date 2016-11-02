
class Chain {
    name: string;
    stores: Store[];
    constructor(name: string, stores: Store[]) {
        this.name = name;
        this.stores = stores;
    }
}

class ChainDTO {
    public id: number;
    public chainNumber: number;
    public name: string;
    public stores: StoreDTO[];
}


class StoreDTO {
    public id: number;
    public storeId: number;
    public chainId: number;
    public name: string;
    public adress: string;
    public city: string;
    public chain: ChainDTO;
    public prices: Price[];
}

class Chains {
    chains: Chain[];
    datachains: ChainDTO[];
    constructor(private dataService: DataService) {

        this.chains = [];
        this.datachains = [];

        this.dataService.getChains().then(data => {
            for (let chain of data) {
                var stores = [];
                for (let storeDTO of chain.stores) {
                    var store = new Store(storeDTO.adress);
                    stores.push(store);
                }
                var newChain = new Chain(chain.name, stores);
                this.chains.push(newChain);
            }

        });

     
    }

    public getChains() {
        return this.chains;
    }
}
