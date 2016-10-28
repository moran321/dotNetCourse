
using System.Collections.Generic;
using System.Windows.Controls;

using PriceCompare.ViewModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        private DataManager _manager;
        private List<ViewCart> _carts;
        private List<ViewCart> _currentChaincarts;
        private ViewCart selected_cart;
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
            _carts = _manager.GetCartsPrice();


            var cheapest = from c in _carts
                           group c by c.ChainName into grp
                           select grp.First();
            suppliers_listBox.ItemsSource = cheapest;
        }
        /*---------------------------------*/

        private void suppliers_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stores_listBox.Visibility = System.Windows.Visibility.Visible;

            selected_cart = suppliers_listBox.SelectedItem as ViewCart;

            stores_listBox.ItemsSource = _currentChaincarts = (from c in _carts
                                                               where c.ChainName.Equals(selected_cart.ChainName)
                                                               select c).ToList();

        }
        /*---------------------------------*/

        private void stores_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stores_listBox.SelectedIndex < 0)
            {
                return;
            }

            var items = (from i in _carts
                         where i.ChainName.Equals(_currentChaincarts.ElementAt(stores_listBox.SelectedIndex).ChainName)
                         && i.StoreName.Equals(_currentChaincarts.ElementAt(stores_listBox.SelectedIndex).StoreName)
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

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(_carts.First().Items.ToArray());
            File.WriteAllText(@"D:\MyCart.txt", json);
            label1.Content = "Saved!";
        }
        /*---------------------------------*/
    }
}
