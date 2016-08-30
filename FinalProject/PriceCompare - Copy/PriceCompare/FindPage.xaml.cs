using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PriceCompare.ViewModel;


namespace PriceCompare.View

{
    /// <summary>
    /// Interaction logic for FindPage.xaml
    /// </summary>
    public partial class FindPage : Page
    {
        private bool isSupplier;
        private List<string> suppliersList;
        private List<string> branchesList;

        DataManager _manager;
        /*---------------------------------*/

        public FindPage(DataManager manager)
        {
            InitializeComponent();
            _manager = manager;
            suppliersList = _manager?.GetSuppliers();

        }
        /*---------------------------------*/


        //show the selection in text box
        private void search_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            branch_textBox.IsEnabled = true;
            if (isSupplier)
            {
                supplier_textBox.Text = search_listBox.SelectedItem?.ToString();
                branch_textBox.IsEnabled = true;
            }
            else
            {
                branch_textBox.Text = search_listBox.SelectedItem?.ToString();
            }
        }
        /*---------------------------------*/


        private void branch_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
        var matchEntries = new List<string>();

        if (suppliersList == null)
        {
            return;
        }
        foreach (string str in suppliersList)
        {
            if (str.ToLower().Contains(branch_textBox.Text.ToLower()))
            {
                matchEntries.Add(str);
            }
        }

        if (matchEntries.Any())
        {
            search_listBox.ItemsSource = matchEntries;
        }
        else
        {
            //none found
        }

        //e.Handled = true;
        */
        }
        /*---------------------------------*/

        private void supplier_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var matchEntries = new List<string>();

            if (suppliersList == null)
            {
                return;
            }
            foreach (string str in suppliersList)
            {
                if (str.ToLower().Contains(supplier_textBox.Text.ToLower()))
                {
                    matchEntries.Add(str);
                }
            }

            if (matchEntries.Any())
            {
                search_listBox.ItemsSource = matchEntries;
            }
            else
            {
                //none found
            }

            //e.Handled = true;
        }
        /*---------------------------------*/

        //get data from manager
        private void supplier_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isSupplier = true;
            supplier_textBox.Text = "";
            search_listBox.ItemsSource = suppliersList;
        }
        /*---------------------------------*/


        private void branch_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isSupplier = false;
            branch_textBox.Text = "";
            branchesList = _manager.GetStoreNamesOfChain(supplier_textBox.Text);
            search_listBox.ItemsSource = branchesList;
        }
        /*---------------------------------*/

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (suppliersList.Contains(supplier_textBox.Text))
            {
                _manager.SetSelection(supplier_textBox.Text, branch_textBox.Text);
                var page2 = new SelectItemsPage(_manager);
                this.NavigationService.Navigate(page2);
            }
        }
        /*---------------------------------*/


        
    }
}
