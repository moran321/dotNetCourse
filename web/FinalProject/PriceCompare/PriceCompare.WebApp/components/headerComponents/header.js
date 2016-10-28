var HeaderCtrl = (function () {
    function HeaderCtrl(cartService) {
        this.cartService = cartService;
    }
    return HeaderCtrl;
}());
app.component("maHeader", {
    templateUrl: "components/headerComponents/header.html",
    //bindings: {
    //},
    controller: HeaderCtrl
});
//# sourceMappingURL=header.js.map