using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication
{
    public static class DrawChart
    {
        public static string DrawTasks(string TaskTitle, int TaskPriority, int TaskDuration, ObservableCollection<Rectangle> TaskBars, string Titles)
        {
            var rectangle = new Rectangle();
            rectangle.Name = TaskTitle;
            rectangle.Width = TaskDuration;
            rectangle.Height = 15;
            rectangle.Fill = PickBrush();
            rectangle.StrokeThickness = 1;

            switch (TaskPriority)
            {
                case 1:
                    rectangle.Stroke = Brushes.Green;
                    break;
                case 2:
                    rectangle.Stroke = Brushes.Gray;
                    break;
                case 3:
                    rectangle.Stroke = Brushes.Red;
                    break;
            }

            Canvas.SetLeft(rectangle, 100);
            Canvas.SetTop(rectangle, TaskBars.Count * 16);
            TaskBars.Add(rectangle);

            Titles += rectangle.Name + " (" + rectangle.Width + "p.)" + "\n";

            return Titles;
        }

        public static string DrawScheduledTasks(string Titles, List<Scheduler.Task> orderedList, ObservableCollection<Rectangle> TaskBars, List<Scheduler.Task> Tasks, List<string> ExcludedTasks, int[] included)
        {
            MessageBox.Show("Ütemezés elvégezve!", "", MessageBoxButton.OK, MessageBoxImage.Information);

            Titles += "\nÜtemezési sorrend: ";

            double plusWidth = 0;

            for (int i = 0; i < orderedList.Count; i++)
            {
                if (included[i] == 1)
                {
                    var rectangleCopy = TaskBars.First(x => x.Name == orderedList[i].TaskTitle);
                    var rectangle = new Rectangle();

                    rectangle.Name = rectangleCopy.Name;
                    rectangle.Width = rectangleCopy.Width;
                    rectangle.Height = 15;
                    rectangle.Fill = rectangleCopy.Fill;
                    rectangle.Stroke = rectangleCopy.Stroke;
                    rectangle.StrokeThickness = 1;

                    Canvas.SetLeft(rectangle, 100 + plusWidth);
                    Canvas.SetTop(rectangle, Tasks.Count * 16 + 40);

                    Titles += rectangle.Name + " - ";

                    TaskBars.Add(rectangle);

                    plusWidth = plusWidth + rectangle.Width;
                }

                else
                {
                    ExcludedTasks.Add(orderedList[i].TaskTitle);
                }
            }

            if (ExcludedTasks.Count == 0)
            {
                Titles += "\n\n\nMinden feladat belefért az időkeretbe!";
            }

            else
            {   
                Titles += "\n\n\nKimaradt feladatok: ";
                foreach (var item in ExcludedTasks)
                {
                    Titles += "\n" + item;
                }
            }

            return Titles;
        }

        private static Brush PickBrush()
        {
            var colors = new List<SolidColorBrush>
            {
                Brushes.Beige,
                Brushes.BlanchedAlmond,
                Brushes.SeaShell,
                Brushes.LightYellow,
                Brushes.Honeydew,
                Brushes.Linen,
                Brushes.WhiteSmoke,
                Brushes.MintCream,
                Brushes.Azure,
                Brushes.LightCyan,
                Brushes.LightSteelBlue,
                Brushes.PaleTurquoise,
                Brushes.Lavender,
                Brushes.LavenderBlush,
                Brushes.MistyRose,
                Brushes.Thistle,
            };

            Brush result = Brushes.Transparent;
            var random = new Random();
            int index = random.Next(colors.Count);
            result = (colors[index]);

            return result;
        }
    }
}
