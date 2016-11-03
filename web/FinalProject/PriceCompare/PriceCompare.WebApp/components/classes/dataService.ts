class DataService {
    private chains: ng.IPromise<ChainDTO[]>;

    constructor(private $http: ng.IHttpService, private userSelectionService: UserSelectionService) {
        this.chains  = this.getChainsFromServer();
    }

    public getChains(): ng.IPromise<ChainDTO[]> {
        return this.chains;
    }

    public getChainsFromServer(): ng.IPromise<ChainDTO[]> {
        return this.$http.get("http://localhost:7407/api/chains")
            .then((response) => {
                console.log(response.data);
                return response.data;
               
            });
    }


    public getItems(): ng.IPromise<ItemDTO[]> {
        return this.$http.get("http://localhost:7407/api/items?chainName=" +
            this.userSelectionService.storesToCompare[0].chainName
            + "storeName=" + this.userSelectionService.storesToCompare[0] )
            .then((response) => {
                console.log(response.data);
                return response.data;

            });
    }

}