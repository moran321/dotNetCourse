var Item = (function () {
    function Item(name, description, packageQuant) {
        this.name = name;
        this.description = description;
        this.pic = "https://www.iaap-hq.org/global_graphics/default-store-350x350.jpg";
        this.packageQuant = packageQuant;
    }
    return Item;
}());
var ItemDTO = (function () {
    function ItemDTO() {
    }
    return ItemDTO;
}());
