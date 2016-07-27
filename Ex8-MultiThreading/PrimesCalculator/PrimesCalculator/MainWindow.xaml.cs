using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Moran Ankori*/
/* Multithreading 1 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Threading;

namespace PrimesCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventWaitHandle _cancelEvent;
        CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _cancelEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "_CancelCalculation");
            cancellationTokenSource = new CancellationTokenSource();

        }
        /*****************************************/

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /*****************************************/

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.IsEnabled = false;
            cancel_button.IsEnabled = true;

            if (CheckValidation())
            {
                int lower = Convert.ToInt32(start_range_box.Text);
                int upper = Convert.ToInt32(end_range_box.Text);
                label.Content = "Calculation in progress";
                try {
                    var result = await CalcPrimesAsync(lower, upper);
                    _cancelEvent.WaitOne(0);
                    //proceed after finish the await
                    label.Content = "Calculation finished";
                    UpdateList(result);

                }
                catch (OperationCanceledException ex)
                {

                    label.Content = "Calculation canceled";
                    listBox.Items.Clear();
                    listBox.IsEnabled = false;
                    calculate_button.IsEnabled = true;
                    cancel_button.IsEnabled = false;

                }
               

            }
            else
            {
                label.Content = "Invalid input";
            }
            listBox.EndInit();
            listBox.IsEnabled = true;
            cancel_button.IsEnabled = false;
        }
        /*****************************************/

        private bool CheckValidation()
        {

            int lower = 0, upper = 0;
            if (int.TryParse(start_range_box.Text, out lower)
                && int.TryParse(end_range_box.Text, out upper))
            {
                if (upper <= lower || lower < 0 || upper < 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        /*****************************************/


        public async Task<IEnumerable<int>> CalcPrimesAsync(int lower, int upper)
        {
            var primes = new List<int>();
            var cancellationToken = cancellationTokenSource.Token;
            bool isPrime;


            try
            {
                await Task.Run(() =>
                {
                    for (int i = lower; i <= upper; i++)
                    {
                        // cancellationToken.ThrowIfCancellationRequested();
                        if (cancellationToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException("canceled");
                        }

                            isPrime = true;
                        for (int j = 2; j < i; j++)
                        {
                            if (i % j == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }

                        if (isPrime)
                        {
                            primes.Add(i);
                        }
                    }
                }, cancellationToken);
            }
            catch (OperationCanceledException e)
            {

                label.Content = "Calculation canceled";
                listBox.Items.Clear();
                listBox.IsEnabled = false;
                calculate_button.IsEnabled = true;
                cancel_button.IsEnabled = false;

            }

            return primes;
        }
        /*****************************************/


        private void UpdateList(IEnumerable<int> list)
        {
            listBox.Items.Clear();
            foreach (int n in list)
                listBox.Items.Add(n);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            //_cancelEvent.Set();
            cancellationTokenSource.Cancel();
        }
        /*****************************************/
    }
}
