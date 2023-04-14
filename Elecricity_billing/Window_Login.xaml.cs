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
using static Elecricity_billing.Cripto;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Window_Login.xaml
    /// </summary>
    public partial class Window_Login : Window
    {
        Entities entities = new Entities();
        public Window_Login()
        {
            InitializeComponent();
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = entities.Users.FirstOrDefault
                    (x => x.Login == textBox_login.Text && x.Password == passwordBox_password.Password);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Captcha captcha = new Captcha();
                    captcha.ShowDialog();
                    textBox_login.Clear();
                    passwordBox_password.Clear();
                }
                else
                {
                    switch (user.Id_user)
                    {
                        case 1:
                            {
                                MessageBox.Show("Здравствуйте, Администратор!",
                                  "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindow window = new MainWindow();
                                this.Close();
                                window.ShowDialog();
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            {
                                MessageBox.Show("Здравствуйте, Пользователь!",
                                  "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                Window_user windowUser = new Window_user();
                                this.Close();
                                windowUser.ShowDialog();
                            }
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK,
                       MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка! " + Ex.Message.ToString() + " Критическая работа приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void button_registration_Click(object sender, RoutedEventArgs e)
        {
            Window_registration window = new Window_registration();
            this.Close();
            window.ShowDialog();
        }
    }
}
