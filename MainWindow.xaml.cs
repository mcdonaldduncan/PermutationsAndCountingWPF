using PermutationsAndCountingWPF.Services;
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


namespace PermutationsAndCountingWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PermutationServices PermutationService = new PermutationServices();
        FormattingServices FormattingService = new FormattingServices();
        CountingServices CountingService = new CountingServices();

        string input = string.Empty;
        int limit;

        bool validInput;
        bool validLimit;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validate character input while assigning warnings for invalid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Warning == null) return;
            

            if (tb_Input == null || tb_Input.Text.Length != 5)
            {
                validInput = false;
                tb_Warning.Text = "Enter 5 characters";
            }
            else
            {
                validInput = true;
                tb_Warning.Text = "";
                input = tb_Input.Text;
            }
            
            CheckValidState();
        }
        
        /// <summary>
        /// Parse and validate limit while assigning warnings for invalid limit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Limit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_WarningLimit == null) return;


            if (tb_Limit == null || !int.TryParse(tb_Limit.Text, out int _limit))
            {
                validLimit = false;
                tb_WarningLimit.Text = "Enter a number 1-5";
            }
            else
            {
                if (_limit > 5 || _limit < 1)
                {
                    validLimit = false;
                    tb_WarningLimit.Text = "Enter a number 1-5";
                }
                else
                {
                    validLimit = true;
                    tb_WarningLimit.Text = "";
                    limit = _limit;
                }
            }

            CheckValidState();
        }

        /// <summary>
        /// Update display on button click if limit and string have been assigned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (limit == 0 || input == string.Empty) return;

            UpdateDisplay();
        }

        /// <summary>
        /// Check character input and limit state and modify button visibility based on state
        /// </summary>
        void CheckValidState()
        {
            if (validInput && validLimit)
            {
                btn_Submit.IsEnabled = true;
            }
            else
            {
                btn_Submit.IsEnabled = false;
            }
        }

        /// <summary>
        /// process current input via services and assign resulting data to the correct controls
        /// </summary>
        void UpdateDisplay()
        {
            PermutationService.ProcessInput(input, limit, out List<string> permutations, out List<string> ordPartitions, out List<string> combinations);

            tb_Permutations.Text = FormattingService.FormatList(permutations);
            tb_Combinations.Text = FormattingService.FormatList(combinations);
            tb_OrderedPartitions.Text = FormattingService.FormatList(ordPartitions);


            tb_PermExpected.Text = CountingService.PermutationCount(input.Length, limit).ToString();
            tb_PermActual.Text = permutations.Count.ToString();

            tb_ComboExpected.Text = CountingService.CombinationCount(input.Length, limit).ToString();
            tb_ComboActual.Text = combinations.Count.ToString();

            tb_PartExpected.Text = CountingService.OrderedPartitionCount(input).ToString();
            tb_PartActual.Text = ordPartitions.Count.ToString();
        }
    }
}
