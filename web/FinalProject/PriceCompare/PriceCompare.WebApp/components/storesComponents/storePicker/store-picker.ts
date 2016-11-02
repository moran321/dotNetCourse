
class StorePickerCtrl {
    //chains: Chains;
    selectedChain: Chain;
    selectedStore: Store;
    chains: Chains;

    public constructor(private userSelectionService: UserSelectionService,
        private storesService: StoresService, private dataService: DataService) {
        /**/
        this.chains = new Chains(dataService);
        
        /**/
    }

    public getChains() {
        return this.chains.chains;
    }

    public onStoreChanged(index: number) {
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
            } else {
                this.userSelectionService.changeStoreToCompare(this.selectedStore, index);

            }

        }

    }

    public remove(index: number) {
        this.storesService.remove(index);
    }

    public isLast(index: number) {
        if ((this.storesService.numOfStoresToCompare.length) === index && index > 2) {
            return true;
        }
        return false
    }

}
