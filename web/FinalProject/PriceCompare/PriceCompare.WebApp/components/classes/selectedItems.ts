
enum BestOrWorst { regularPrice, bestPrice, worstPrice }

class SelectedItem {
    public item: Item;
    public quantity: number;
    public price: number;
    public bestOrWorst: BestOrWorst;
}
