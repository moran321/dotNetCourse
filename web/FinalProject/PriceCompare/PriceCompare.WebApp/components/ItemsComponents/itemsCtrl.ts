
class ItemsCtrl {

    public selectedText: string;

    constructor(private userSelectionService: UserSelectionService, private dataService: DataService) {
    }

    public isCartEmpty() {
        if (this.userSelectionService.numberOfItems() == 0) {
            return true;
        }
        return false;
    }


    public getItemsInCart(): ViewItem[] {
        return this.userSelectionService.cartItems;
    }

    public getStoreItems(): ViewItem[] {
       // return  this.dataService.getItems();
        return this.userSelectionService.getStoreItems();
       
    }


    public addItem(viewItem: ViewItem) {
        this.userSelectionService.addItem(viewItem);
        this.selectedText = '';
    }

   public removeItem(viewItem: ViewItem) {
        this.userSelectionService.removeItem(viewItem);
    }
}



