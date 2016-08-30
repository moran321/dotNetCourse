using PriceCompare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for SelectItemsPage.xaml
    /// </summary>
    public partial class SelectItemsPage : Page
    {
        DataManager _manager;
        List<ViewItem> _itemsList;
        List<SelectedItem> _selectedItems;
        /*---------------------------------*/

        //specific store
        public SelectItemsPage(DataManager manager)
        {
            _manager = manager;
            _selectedItems = new List<SelectedItem>();
            _itemsList = new List<ViewItem>();
            InitializeComponent();
            InitializeStoreData();
        }

        /*---------------------------------*/

        //all items
        public SelectItemsPage()
        {
            _manager = new DataManager();
            InitializeComponent();
            InitializeData();
        }
        /*---------------------------------*/

        private void InitializeData()
        {
           //  _itemsList = _manager.GetCommonItems();
            // items_listBox.ItemsSource = _itemsList;
        }
        /*---------------------------------*/

        private void InitializeStoreData()
        {
            _itemsList = _manager.GetItemsInStore();
            var names = from r in _itemsList
                        select r.ItemName;
            items_listBox.ItemsSource = names;
        }
        /*---------------------------------*/

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }
        /*---------------------------------*/


        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTextAllowed(quantity_textBox.Text))
            {
                return;
            }
            if (items_listBox.Items.Contains(item_textBox.Text))
            {
                cart_listBox.Items.Add(items_listBox.SelectedItem);

                var entry = (from en in _itemsList
                            where en.ItemName.Equals(items_listBox.SelectedItem)
                            select en).First();
                var item = new SelectedItem() { item = entry, Quantity = Convert.ToInt32(quantity_textBox.Text) };
                //var item = new SelectedItem() { item = _itemsList.ElementAt(items_listBox.SelectedIndex), Quantity = Convert.ToInt32(quantity_textBox.Text) };
                _selectedItems.Add(item);
            }
        }
        /*---------------------------------*/

        private void item_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var matchEntries = new List<string>();

            if (_itemsList == null)
            {
                return;
            }
            foreach (var str in _itemsList)
            {
                if (str.ItemName.ToLower().Contains(item_textBox.Text.ToLower()))
                {
                    matchEntries.Add(str.ItemName);
                }
            }

            if (matchEntries.Any())
            {
                items_listBox.ItemsSource = matchEntries;
            }
            else
            {
                //none found
            }

        }
        /*---------------------------------*/

        private void remove_button_Click(object sender, RoutedEventArgs e)
        {
            if (cart_listBox.SelectedIndex < 0)
            {
                return;
            }
            _selectedItems.RemoveAt(cart_listBox.SelectedIndex);
            cart_listBox.Items.Remove(cart_listBox.SelectedItem);
        }
        /*---------------------------------*/

        private void item_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            item_textBox.Text = "";
        }
        /*---------------------------------*/
        private void continue_button_Click(object sender, RoutedEventArgs e)
        {
            _manager.AddSelectedItems(_selectedItems);
            ResultsPage page2 = new ResultsPage(_manager);
            this.NavigationService.Navigate(page2);
        }
        /*---------------------------------*/
        private void items_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_textBox.Text = items_listBox.SelectedItem?.ToString();
        }
        /*---------------------------------*/


    }
}
