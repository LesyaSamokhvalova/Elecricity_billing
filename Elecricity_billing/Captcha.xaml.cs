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
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Timers;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();
        }

        private void Captcha_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int num = 4;
            string capcha = "";
            int totl = 0;
            do
            {
                int chr = random.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    capcha = capcha + (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                }
            }
            while (true);
            L_captcha.Content = capcha;
        }

        private async void button_enter_Click(object sender, RoutedEventArgs e)
        {
            if ((string)L_captcha.Content == Textbox_captcha.Text)
            {
                MessageBox.Show("Верно!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Captcha_Loaded(sender, e);
                Textbox_captcha.Clear();

                Random random = new Random();
                int num = 4;
                string capcha = "";
                int totl = 0;
                do
                {
                    int chr = random.Next(48, 123);
                    if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                    {
                        capcha = capcha + (char)chr;
                        totl++;
                        if (totl == num)
                            break;
                    }
                }
                while (true);
                L_captcha.Content = capcha;

                button_enter.IsEnabled = false;
                for (int i = 10; i >= 0; i--)
                {
                    button_enter.Content = String.Format("Осталось {0} секунд", i);
                    await Task.Delay(1000);
                }
                button_enter.Content = "Ок";
                button_enter.IsEnabled = true;
            }
        }

        private void button_refresh_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int num = 4;
            string capcha = "";
            int totl = 0;
            do
            {
                int chr = random.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    capcha = capcha + (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                }
            }
            while (true);
            L_captcha.Content = capcha;
        }
    }
}

