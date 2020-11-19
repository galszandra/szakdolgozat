using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication
{
    class OptimizeCommand : ICommand
    {
        public OptimizeCommand(MainViewModel viewmodel)
        {
            mainViewModel = viewmodel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (mainViewModel.Tasks.Count < 2)
            {
                System.Windows.Forms.MessageBox.Show("Még nincs (elég) feladat hozzáadva!", "Hibaüzenet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                int capacity = 480;
                int n = mainViewModel.Tasks.Count;
                int[] included = new int[n];

                var orderedList = KnapSack.OrderTasks(mainViewModel.Tasks);

                KnapSack.KnapSackAlgorithm(capacity, orderedList, n, included);

                mainViewModel.Titles = DrawChart.DrawScheduledTasks(mainViewModel.Titles, orderedList, mainViewModel.TaskBars, mainViewModel.Tasks, ExcludedTasks, included);
            }
        }

        private List<string> ExcludedTasks = new List<string>();

        private MainViewModel mainViewModel;
    }
}