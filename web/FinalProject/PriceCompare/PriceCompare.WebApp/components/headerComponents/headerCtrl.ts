
class HeaderCtrl {
    constructor(private userSelectionService: UserSelectionService) {
    }

    public numberOfItems() {
        return this.userSelectionService.numberOfItems();
    }
}

