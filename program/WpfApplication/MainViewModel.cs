using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfApplication
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            AddTaskCommand = new AddTaskCommand(this);
            OptimizeCommand = new OptimizeCommand(this);
        }

        public ICommand AddTaskCommand
        {
            get;
            set;
        }

        public ICommand OptimizeCommand
        {
            get;
            set;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Rectangle> TaskBars { get; } = new ObservableCollection<Rectangle>();

        public List<Scheduler.Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public string TaskTitle
        {
            get { return this.tasktitle; }
            set
            {
                this.tasktitle = value;
                RaisePropertyChanged(nameof(TaskTitle));
            }
        }

        public int TaskPriority
        {
            get { return this.taskpriority; }
            set
            {
                this.taskpriority = value;
                RaisePropertyChanged(nameof(TaskPriority));
            }
        }

        public int TaskDuration
        {
            get { return this.taskduration; }
            set
            {
                this.taskduration = value;
                RaisePropertyChanged(nameof(TaskDuration));
            }
        }

        public string Titles
        {
            get { return this.titles; }
            set
            {
                this.titles = value;
                RaisePropertyChanged(nameof(Titles));
            }
        }

        private List<Scheduler.Task> tasks = new List<Scheduler.Task>();

        private string tasktitle;

        private int taskpriority;

        private int taskduration;

        private string titles;
    }
}
