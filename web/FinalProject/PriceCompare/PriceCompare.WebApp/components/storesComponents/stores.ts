﻿class Store {
    items: Item[];
    address: string;

    constructor(address: string) {
        this.items = [
            new Item("במבה", 2.9, "במה אסם 100גר"),
            new Item("ביסלי", 3.9, "ביסלי גריל"),
            new Item("קליק", 7, "קליק כריות"),
            new Item("קפה שחור", 10, "קפה שחור עלית"),
            new Item("חלב", 5.7, "תנובה 3%"),];
        this.address = address;
    }
}

//class StoreCtrl {
    
//    constructor(private cartService: CartService) {
      
//    }

//}




