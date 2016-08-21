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

namespace PriceCompare
{
    /// <summary>
    /// Interaction logic for SelectItemsPage.xaml
    /// </summary>
    public partial class SelectItemsPage : Page
    {
        DataManager _manager;
        List<string> _itemsList;

        /*---------------------------------*/

        public SelectItemsPage(DataManager manager)
        {
            _manager = manager;
            InitializeComponent();
            InitializeData();
        }

        /*---------------------------------*/


        private void InitializeData()
        {
            _itemsList = _manager.GetItems();
            items_listBox.ItemsSource = _itemsList;
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
            if (_itemsList.Contains(item_textBox.Text))
            {
                //var item = string.Format(quantity_textBox.Text + "\t" + items_listBox.SelectedItem);
                cart_listBox.Items.Add(items_listBox.SelectedItem);
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
            foreach (string str in _itemsList)
            {
                if (str.ToLower().Contains(item_textBox.Text.ToLower()))
                {
                    matchEntries.Add(str);
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
            _manager.SetSelecteditems(cart_listBox.Items.Cast<string>());
            _manager.CalculateCartsPrice();
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
