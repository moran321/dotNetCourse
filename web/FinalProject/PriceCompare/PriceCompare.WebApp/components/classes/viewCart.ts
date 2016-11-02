class ViewCart {
    public chainName: string;
    public store: string;
    public items: SelectedItem[];
    public cartPrice: number;

    constructor() {
        this.items = [];
    }

    public getTotalPrice() {
        
    }

    public addItem(item: SelectedItem) {
        this.items.push(item);
    }
}