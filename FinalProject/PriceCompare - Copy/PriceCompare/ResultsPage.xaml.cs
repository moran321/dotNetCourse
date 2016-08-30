
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;

using PriceCompare.ViewModel;

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {       
        private DataManager _manager;
        /*---------------------------------*/

        public ResultsPage(DataManager manager)
        {
            InitializeComponent();   
            _manager = manager;
            DisplayResults();
        }
        /*---------------------------------*/
        
            
            /*
        private void CalculateCartPrice()
        {
            //foreach supplier add item cost
            var list = new List<string>();
            List<Cart> carts = _manager.Carts;
            foreach (Cart c in carts)
            {
                list.Add(c.ToString());
            }
            suppliers_listBox.ItemsSource = list;
        } 
        /*---------------------------------*/

        private void DisplayResults()
        {
            suppliers_listBox.ItemsSource = _manager.GetCartsPrice();
        }
        /*---------------------------------*/


    }
}
