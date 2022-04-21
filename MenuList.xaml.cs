using CrudExample.ViewModel;
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

namespace CrudExample
{
    /// <summary>
    /// Lógica de interacción para MenuList.xaml
    /// </summary>
    public partial class MenuList : Page
    {
        public MenuList()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            List<StudentViewModel> list = new List<StudentViewModel>();
            using (Model.testEntities db = new Model.testEntities())
            {
                list = (from d in db.student
                        select new StudentViewModel
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Age = d.Age,
                            Course = d.Course
                        }).ToList();
                StudentDG.ItemsSource = list;
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.StaticMainFrame.Content = new StudentForm();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            var form = new StudentForm(id);
            MainWindow.StaticMainFrame.Content = form;

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            using (Model.testEntities db = new Model.testEntities())
            {
                var student = db.student.Find(id);
                db.student.Remove(student);
                db.SaveChanges();
            }
            Refresh();
        }
    }
}
