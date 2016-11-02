class Store {
    chainName: string;
    prices: Price[];
    address: string;

    constructor(address: string) {
        this.prices = this.generateMockItems();

        this.address = address;
    }

    public getPrice(item: Item) {
        var itemPrice = this.prices.filter(x => x.item.name === item.name)[0]; //change to item code compare
        if (itemPrice === null) {
            return;
        }
        return itemPrice.price;
    }

    private generateMockItems(): Price[] {

        var prices = [
            new Price((new Item("במבה", "במה אסם", "250 גר")), Math.floor((Math.random() * 5.5) + 1)),
            new Price((new Item("ביסלי", "ביסלי גריל", "250 גר")), Math.floor((Math.random() * 3) + 1)),
            new Price((new Item("קליק", "קליק כריות", "300 גר")), Math.floor((Math.random() * 10) + 1)),
            new Price((new Item("קפה שחור", "קפה שחור עלית", "שקית 500 גר")), Math.floor((Math.random() * 15) + 5)),
            new Price((new Item("חלב", "תנובה 3%", "קרטון ליטר")), 5.8),
            new Price((new Item("קורנפלקס", "פתיתי תירס תלמה", "500 גר")), Math.floor((Math.random() * 15) + 10))
        ];
        return prices;
    }

}

