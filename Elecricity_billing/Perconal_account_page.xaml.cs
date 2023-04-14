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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Elecricity_billing.functions;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Perconal_account_page.xaml
    /// </summary>
    public partial class Perconal_account_page : Page
    {
        Entities entities = new Entities();
        public Perconal_account_page()
        {
            InitializeComponent();
            foreach (var percacc in entities.Perconal_account)
                ListBox_personal_acc.Items.Add(percacc);

        }

        private void ListBox_personal_acc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_acc = ListBox_personal_acc.SelectedItem as Perconal_account;
            if (selected_acc != null)
            {
                TextBox_num_perconal_acc.Text = Convert.ToString(selected_acc.Id_counter);
                TextBox_payer.Text = selected_acc.Payer;
                TextBox_number_lot.Text = Convert.ToString(selected_acc.Number_lot);
                TextBox_land_area.Text = Convert.ToString(selected_acc.Land_area);
                TextBox_id_counter.Text = Convert.ToString(selected_acc.Id_counter);
            }
            else
            {
                TextBox_num_perconal_acc.Text = "";
                TextBox_payer.Text = "";
                TextBox_number_lot.Text = "";
                TextBox_land_area.Text = "";
                TextBox_id_counter.Text = "";
            }
        }
                
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            var pers_acc = ListBox_personal_acc.SelectedItem as Perconal_account;
            if(TextBox_num_perconal_acc.Text == "" || TextBox_payer.Text == "" || TextBox_number_lot.Text == "" ||
                TextBox_land_area.Text == "" || TextBox_id_counter.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (pers_acc == null)
                {
                    pers_acc= new Perconal_account();
                    entities.Perconal_account.Add(pers_acc);

                    ListBox_personal_acc.Items.Add(pers_acc);
                    MessageBox.Show("Запись добавлена!", "Операция выполнена", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                pers_acc.Id_personal_account = TextBox_num_perconal_acc.Text;
                pers_acc.Payer = TextBox_payer.Text;
                pers_acc.Number_lot = TextBox_number_lot.Text;
                pers_acc.Land_area = Convert.ToDecimal(TextBox_land_area.Text);
                pers_acc.Id_counter = Convert.ToInt32(TextBox_id_counter.Text);
                ListBox_personal_acc.Items.Refresh();
            }
        }

        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete_persacc = ListBox_personal_acc.SelectedItem as Perconal_account;
                var exist = (from structure in entities.Counter
                             where structure.Id_counter== delete_persacc.Id_counter
                             select structure).First();
                MessageBox.Show("Запись удалить нельзя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                var rez = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rez == MessageBoxResult.No)
                    return;
                var delete_persacc = ListBox_personal_acc.SelectedItem as Perconal_account;
                if(delete_persacc != null)
                {
                    entities.Perconal_account.Remove(delete_persacc);
                    entities.SaveChanges();
                    TextBox_num_perconal_acc.Clear();
                    TextBox_payer.Clear();
                    TextBox_number_lot.Clear();
                    TextBox_land_area.Clear();
                    TextBox_id_counter.Clear();
                    ListBox_personal_acc.Items.Refresh();
                }
            }
        }

        private void TextBox_seurch_TextChanged(object sender, EventArgs e)
        {
            List<object> items = new List<object>();

            if (TextBoxGet.GetLenght(TextBox_seurch) > 0)
            {
                if(Regex.IsMatch(TextBox_seurch.Text, "[а-яА-ЯеЁ]"))
                {
                    foreach (var item in entities.Perconal_account)
                    {

                        string payer = item.Payer;

                        int matches = Regex.Match(payer.ToLower(), TextBox_seurch.Text.ToLower()).Length;

                        if (matches > 0)
                        {
                            items.Add(new ListBoxItem()
                            {
                                Content = $"{item.Id_personal_account}. {item.Payer} {item.Number_lot} " +
                                $"{item.Land_area} {item.Id_counter}",
                                Tag = $"{item.Id_personal_account}"
                            });
                        }
                    }
                }
                else
                {
                    foreach (var item in entities.Perconal_account)
                    {

                        string number = item.Number_lot;

                        int matches = Regex.Match(number.ToLower(), TextBox_seurch.Text.ToLower()).Length;

                        if (matches > 0)
                        {
                            items.Add(new ListBoxItem()
                            {
                                Content = $"{item.Id_personal_account}. {item.Payer} {item.Number_lot} " +
                                $"{item.Land_area} {item.Id_counter}",
                                Tag = $"{item.Id_personal_account}"
                            });
                        }
                    }
                }

                ListBox_personal_acc.Items.Clear();

                foreach (var item in items)
                {
                    ListBox_personal_acc.Items.Add(item);
                }

                items.Clear();

                return;
            }

            ListBox_personal_acc.Items.Clear();

            for (int i = 0; i < entities.Perconal_account.Count(); i++)
            {
                ListBox_personal_acc.Items.Add(new ListBoxItem()
                {
                    Content = $"{entities.Perconal_account.ToArray()[i].Id_personal_account}. { entities.Perconal_account.ToArray()[i].Payer} " +
                    $"{ entities.Perconal_account.ToArray()[i].Number_lot} { entities.Perconal_account.ToArray()[i].Land_area} { entities.Perconal_account.ToArray()[i].Id_counter}",
                    Tag = $"{entities.Perconal_account.ToArray()[i].Id_personal_account}"
                });
            }
        }
    }
}
