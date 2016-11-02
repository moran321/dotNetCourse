
class StoresCtrl {

    public constructor(private storesService: StoresService) { }

    public addStore() {
        this.storesService.addStore();
    }

}




