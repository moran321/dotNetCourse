using System;
using System.Collections.Generic;
using System.Windows.Controls;
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
