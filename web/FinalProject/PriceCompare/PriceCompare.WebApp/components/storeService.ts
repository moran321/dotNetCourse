class StoresService {
    numOfStoresToCompare: number[];

    public constructor() {
        this.numOfStoresToCompare = [1, 2];
    }

    public addStore() {
        //if (this.numOfStoresToCompare.length >= this.chains.length) {
        //    return;
        //}
        this.numOfStoresToCompare.push(this.numOfStoresToCompare.length + 1);
    }

    public remove(index: number) {
        this.numOfStoresToCompare.splice(index - 1, 1);
    }

}
