using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Elecricity_billing.functions;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Window_registration.xaml
    /// </summary>
    public partial class Window_registration : Window
    {
        string pattern = @"[0-9a-z]";

        Entities entities = new Entities();
        Users user = new Users();

        public Window_registration()
        {
            InitializeComponent();
        }

        private void button_registration_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxGet.GetLenght(textBox_login) > 0 && TextBoxGet.GetLenght(null, passwordBox_password) > 0 && TextBoxGet.GetLenght(null, passwordBox_password2) > 0)
            {
                if (Regex.IsMatch(textBox_login.Text, pattern))
                {
                    if(passwordBox_password.Password == passwordBox_password2.Password)
                    {
                        string login_ = TextBoxGet.GetText(textBox_login);

                        var log = (from item in entities.Users where item.Login == login_ select item).Count();

                        if (log > 0)
                        {
                            MessageBox.Show("Этот пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        try
                        {
                            user.Login = TextBoxGet.GetText(textBox_login);
                            user.Password = TextBoxGet.GetText(null, passwordBox_password);

                            entities.Users.Add(user);

                            if (entities.SaveChanges() > 0)
                            {
                                Window_Login main = new Window_Login();
                                this.Close();
                                main.ShowDialog();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ошибка");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректное имя пользователя");
                }
            }
            else
            {
                MessageBox.Show("Not all data has been entered");
            }            
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            Window_Login log = new Window_Login();
            this.Close();
            log.ShowDialog();
            
        }
    }
}
