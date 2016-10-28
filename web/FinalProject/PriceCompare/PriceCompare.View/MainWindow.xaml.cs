/* Moran Ankori */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PriceCompare.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //navigate to the main page
            var page = new SearchTypePage();
            mainFrame.Navigate(page);        
        }     

    }
}
