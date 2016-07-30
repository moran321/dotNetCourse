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
using System.IO;


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
           
            cancel_button.IsEnabled = true;

            if (CheckValidation())
            {
                int lower = Convert.ToInt32(start_range_box.Text);
                int upper = Convert.ToInt32(end_range_box.Text);
                label.Content = "Calculation in progress";

                var result = await CalcPrimesAsync(lower, upper);
                _cancelEvent.WaitOne(0);
                //proceed after finish the await
               
            //    UpdateList(result);
                WriteResultsToFile(result);
            }
            else
            {
                label.Content = "Invalid input";
            }

            cancel_button.IsEnabled = false;
        }


        /*****************************************/

        private bool CheckValidation()
        {
            string fileName = output_file_name.Text;

            var isValid = !string.IsNullOrEmpty(fileName) &&
              fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) < 0;

            if (!isValid)
            {
                return false;
            }

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
             
                calculate_button.IsEnabled = true;
                cancel_button.IsEnabled = false;
                return primes;
            }
         //   label.Content = "Calculation finished.";
            return primes;
        }
        /*****************************************/


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
        /*****************************************/

        //lab 4 : Write the result to a file with the name given inside the "Output File" textbox
        private void WriteResultsToFile(IEnumerable<int> result)
        {
            string fileName = string.Format(output_file_name.Text+".txt");
            
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    foreach (int prime in result) {
                        writer.WriteLine(prime);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File Not Found");
                Console.WriteLine("Exception: " + e);
                return;
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e);
                return;
            }
            label.Content = $"File {fileName} created with results";
        }
        /*****************************************/
    }
}
