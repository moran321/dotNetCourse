

class Item {
    Pic: string;
    Name: string;
    Price: number;
    Description: string;

    constructor(name: string, price: number, description: string) {
        this.Name = name;
        this.Price = price;
        this.Description = description;
        this.Pic = "https://www.iaap-hq.org/global_graphics/default-store-350x350.jpg";
    }

}

class ItemsCtrl {

    constructor(private cartService: CartService) {

    }
}


app.component("maItems", {
    templateUrl: "components/itemsComponents/items.html",
    controller: ItemsCtrl
});
