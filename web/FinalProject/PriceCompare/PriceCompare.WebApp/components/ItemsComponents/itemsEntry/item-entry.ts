
class ItemEntryCtrl {
    public onButtonClick: Function;
    public item: ViewItem;
    public buttonText: string;
    public quantity: number; 

    constructor() {      
    }

    public onClick() {
        this.onButtonClick({ item: this.item });
    }
}

