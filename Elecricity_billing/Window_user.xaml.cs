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
using System.Windows.Shapes;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Window_user.xaml
    /// </summary>
    public partial class Window_user : Window
    {
        public Window_user()
        {
            InitializeComponent();
        }

        private void Button_First_list_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new First_page());
        }

        private void Button_Counter_reading_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Counter_reading_page());
        }

        private void Button_escape_Click(object sender, RoutedEventArgs e)
        {
            Window_Login window_Login = new Window_Login();
            this.Close();
            window_Login.ShowDialog();
        }
    }
}
