
class HeaderCtrl {

    constructor(private cartService: CartService) {

    }

}


app.component("maHeader", {
    templateUrl: "components/headerComponents/header.html",
    //bindings: {

    //},
    controller: HeaderCtrl
});


