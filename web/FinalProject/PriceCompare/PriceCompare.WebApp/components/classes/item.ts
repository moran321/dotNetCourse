class Item {
    pic: string;
    name: string;
    packageQuant: string;
    description: string;

    constructor(name: string, description: string, packageQuant: string) {
        this.name = name;
        this.description = description;
        this.pic = "https://www.iaap-hq.org/global_graphics/default-store-350x350.jpg";
        this.packageQuant = packageQuant;
    }

}

class ItemDTO {
    public itemCode: number;
    public name: string;
    public unitQuantity: string;
}