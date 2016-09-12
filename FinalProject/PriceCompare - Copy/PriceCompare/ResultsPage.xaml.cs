
using System.Collections.Generic;
using System.Windows.Controls;

using PriceCompare.ViewModel;
using System.Linq;

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {       
        private DataManager _manager;
        private List<ViewCart> _carts;
        /*---------------------------------*/

        public ResultsPage(DataManager manager)
        {
            InitializeComponent();   
            _manager = manager;
            DisplayResults();
        }
        /*---------------------------------*/

        private void DisplayResults()
        {
            suppliers_listBox.ItemsSource = _carts = _manager.GetCartsPrice();
        }

        private void suppliers_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cheapest:

            var items = (from i in _carts
                        where i.ChainName.Equals(_carts.ElementAt(suppliers_listBox.SelectedIndex).ChainName) 
                        && i.StoreName.Equals(_carts.ElementAt(suppliers_listBox.SelectedIndex).StoreName)
                        select i.Items).First();

            var best = (from i in items
                        orderby i.item.ItemPrice ascending
                        select i).Take(3);

            bestItems_listBox.ItemsSource = best;

            //most expensive:
            var worst = (from i in items
                        orderby i.item.ItemPrice descending
                        select i).Take(3);
       
            worsItems_listBox.ItemsSource = worst;

            stackPanel_cheapest.Visibility = System.Windows.Visibility.Visible;
            stackPanel_expensive.Visibility = System.Windows.Visibility.Visible;

        }

    }
}
