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
    /// Lógica de interacción para StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Page
    {
        public int id = 0;
        public StudentForm(int id = 0)
        {
            InitializeComponent();
            this.id = id;
            if (this.id != 0)
            {
                using (Model.testEntities db = new Model.testEntities())
                {
                    var student = db.student.Find(id);
                    txtName.Text = student.Name.ToString();
                    txtAge.Text = student.Age.ToString();
                    txtCourse.Text = student.Course.ToString();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (id == 0)
            {
                using (Model.testEntities db = new Model.testEntities())
                {
                    var student = new Model.student();
                    student.Name = txtName.Text;
                    student.Age = int.Parse(txtAge.Text);
                    student.Course = txtCourse.Text;

                    db.student.Add(student);
                    db.SaveChanges();
                    MainWindow.StaticMainFrame.Content = new MenuList();
                }
            }
            else 
            {
                using (Model.testEntities db = new Model.testEntities())
                {
                    var student = db.student.Find(id);
                    student.Name = txtName.Text;
                    student.Age = int.Parse(txtAge.Text);
                    student.Course = txtCourse.Text;

                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MainWindow.StaticMainFrame.Content = new MenuList();
                }
            }
            
        }
    }
}
