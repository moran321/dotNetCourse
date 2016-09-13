using System;
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
using System.Threading;

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private DataManager _manager;
        List<ViewItem> _itemsList;

        public LoadingPage(DataManager manager)
        {
            InitializeComponent();
            _manager = manager;
            Thread.Sleep(50);
            InitializeStoreData();         
        }


        private void InitializeStoreData()
        {

            _itemsList = _manager.GetItemsInStore();
            Thread.Sleep(5);
            var page = new SelectItemsPage(_manager);
            this.NavigationService.Navigate(page);

        }
        /*---------------------------------*/



    }
}
