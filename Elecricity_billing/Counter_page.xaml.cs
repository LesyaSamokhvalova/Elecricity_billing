using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Counter_page.xaml
    /// </summary>
    public partial class Counter_page : Page
    {
        Entities entities = new Entities();

        string destinationDirectory;
        public Counter_page()
        {
            InitializeComponent();
            foreach(var couner in entities.Counter)
                ListBox_counter.Items.Add(couner);
            foreach (var combo in entities.Tarif)
                ComboBox_id_tarif.Items.Add(combo);
        }

        private void ListBox_counter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_counter = ListBox_counter.SelectedItem as Counter;
            if (selected_counter != null)
            {

                TextBox_number_counter.Text = Convert.ToString(selected_counter.Id_counter);
                TextBox_mark.Text = selected_counter.Mark;
                TextBox_Serial_number.Text = selected_counter.Serial_number;
                TextBox_Seal_number.Text = selected_counter.Seal_number;
                TextBox_Id_tarif.Text = Convert.ToString(selected_counter.Id_counter);
                Image_counter.Source = new BitmapImage(new Uri(selected_counter.Photo, UriKind.RelativeOrAbsolute));
                ListBox_counter.Items.Refresh();
            }
        }

        private void Button_add_photo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
                openFileDialog.ShowDialog();

                string fileToCopy = openFileDialog.FileName;

                string workingDirectory = Environment.CurrentDirectory;
                destinationDirectory = $"{Directory.GetParent(workingDirectory).Parent.Parent.FullName}\\Electricity_billing\\imgs\\" + System.IO.Path.GetFileName(fileToCopy);

                File.Copy(fileToCopy, destinationDirectory);

                Image_counter.Source = new BitmapImage(new Uri(destinationDirectory, UriKind.RelativeOrAbsolute));
            }
            catch (Exception)
            {
            }
        }

        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            string path = destinationDirectory != null ? destinationDirectory.Substring(destinationDirectory.IndexOf("\\imgs")) : null;

            var counter = ListBox_counter.SelectedItem as Counter;
            if (TextBox_number_counter.Text == "" || TextBox_mark.Text == "" || TextBox_Serial_number.Text == "" ||
                TextBox_Seal_number.Text == "" || TextBox_Id_tarif.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (counter == null)
                {
                    counter = new Counter();
                    entities.Counter.Add(counter);

                    ListBox_counter.Items.Add(counter);
                    MessageBox.Show("Запись добавлена!", "Операция выполнена", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                counter.Id_counter = Convert.ToInt32(TextBox_number_counter.Text);
                counter.Mark = TextBox_mark.Text;
                counter.Serial_number = TextBox_Serial_number.Text;
                counter.Seal_number = TextBox_Seal_number.Text;
                counter.Id_tarif = Convert.ToInt32(TextBox_Id_tarif.Text);
                counter.Photo = path;

                entities.Counter.Add(counter);
                entities.SaveChanges();

                ListBox_counter.Items.Refresh();

                TextBox_number_counter.Clear();
                TextBox_mark.Clear();
                TextBox_Serial_number.Clear();
                TextBox_Seal_number.Clear();
                TextBox_Id_tarif.Clear();
            }
        }

        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete_counter = ListBox_counter.SelectedItem as Counter;
                var exist = (from structure in entities.Perconal_account
                             where structure.Id_counter == delete_counter.Id_counter
                             select structure).First();
                MessageBox.Show("Запись удалить нельзя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                var rez = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rez == MessageBoxResult.No)
                    return;
                var delete_counter = ListBox_counter.SelectedItem as Counter;
                if (delete_counter != null)
                {
                    entities.Counter.Remove(delete_counter);
                    entities.SaveChanges();
                    TextBox_number_counter.Clear();
                    TextBox_mark.Clear();
                    TextBox_Serial_number.Clear();
                    TextBox_Seal_number.Clear();
                    TextBox_Id_tarif.Clear();
                    ListBox_counter.Items.Refresh();
                    entities.SaveChanges();
                }
            }
        }

        private void poisk_button_Click(object sender, RoutedEventArgs e)
        {
            ListBox_counter.Items.Clear();
            var filter = ComboBox_id_tarif.SelectedItem as Tarif;
            if (filter != null)
            {
                
                foreach (var filterrrs in entities.Counter)
                {
                    if (filter.Id_tarif == filterrrs.Id_tarif)
                    {
                        ListBox_counter.Items.Add(filterrrs);
                    }
                    
                }
            }
            else
                MessageBox.Show("Выбирете параметр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void delete_filter_button_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_id_tarif.SelectedIndex = -1;
            ListBox_counter.SelectedIndex = -1;
            ListBox_counter.Items.Clear();
            foreach (var couner in entities.Counter)
                ListBox_counter.Items.Add(couner);

        }
    }
}
