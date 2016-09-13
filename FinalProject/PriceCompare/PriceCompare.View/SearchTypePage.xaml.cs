using PriceCompare.ViewModel;
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

namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for SelectGlobalItems.xaml
    /// </summary>
    public partial class SearchTypePage : Page
    {
        DataManager _manager;
        public SearchTypePage()
        {
            _manager = new DataManager();
            InitializeComponent();
            allStores_button.IsEnabled = false; //@@@@@@@@@@@@@ change to true when ready to use
        }

        private void specificStore_button_Click(object sender, RoutedEventArgs e)
        {
            _manager.SearchType = SearchType.SpecificStore;
            var page = new WherePage(_manager);
            this.NavigationService.Navigate(page);
        }

        private void allStores_button_Click(object sender, RoutedEventArgs e)
        {
            _manager.SearchType = SearchType.GlobalItems;
            var page = new WherePage(_manager);
            this.NavigationService.Navigate(page);
        }

        /*
        private void localStores_button_Click(object sender, RoutedEventArgs e)
        {
            _manager.SearchType = SearchType.LocalStores;
            var page = new WherePage(_manager);
            this.NavigationService.Navigate(page);
        }*/

    }

}
