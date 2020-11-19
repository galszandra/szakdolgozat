using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Reflection;

namespace WpfApplication
{
    public class AddTaskCommand : ICommand
    {
        public AddTaskCommand(MainViewModel viewmodel)
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
            if (mainViewModel.TaskTitle == null || TaskTitlesList.Contains(mainViewModel.TaskTitle))
            {
                NullParametersList.Add("Cím");
            }

            if (mainViewModel.TaskPriority == 0)
            {
                NullParametersList.Add("Prioritás");
            }

            if (mainViewModel.TaskDuration == 0 || mainViewModel.TaskDuration > 480)
            {
                NullParametersList.Add("Időtartam");
            }

            if (NullParametersList.Any())
            {
                var message = string.Join("\n", NullParametersList);
                MessageBox.Show(string.Format("A következő érték(ek) hibás(ak):\n{0}", message), "Hibaüzenet", MessageBoxButton.OK, MessageBoxImage.Error);
                NullParametersList.Clear();
            }

            else
            {
                var task = new Scheduler.Task(mainViewModel.TaskTitle, mainViewModel.TaskPriority, mainViewModel.TaskDuration);
                mainViewModel.Tasks.Add(task);
                TaskTitlesList.Add(mainViewModel.TaskTitle);

                NullParametersList.Clear();

               mainViewModel.Titles = DrawChart.DrawTasks(mainViewModel.TaskTitle, mainViewModel.TaskPriority, mainViewModel.TaskDuration, mainViewModel.TaskBars, mainViewModel.Titles);

                MessageBox.Show("Feladat sikeresen hozzáadva! \n" 
                    + "Cím: " + mainViewModel.TaskTitle + "\n" 
                    + "Prioritás: " + mainViewModel.TaskPriority + "\n" 
                    + "Becsült időtartam: " + mainViewModel.TaskDuration + " perc", "", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetViewModelVariables();
            }
        }

        private MainViewModel mainViewModel;

        private List<string> NullParametersList = new List<string>();

        private List<string> TaskTitlesList = new List<string>();
        private void ResetViewModelVariables()
        {
            mainViewModel.TaskTitle = null;
            mainViewModel.TaskPriority = 0;
            mainViewModel.TaskDuration = 0;
        }
    }
}
