
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using PriceCompare.ViewModel;

namespace PriceCompare
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
            CalculateCartPrice();
        }
        /*---------------------------------*/
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
          //  suppliers_listBox = manager.GetSuppliers();
        }
        /*---------------------------------*/


    }
}
