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
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Counter_reading_page.xaml
    /// </summary>
    public partial class Counter_reading_page : Page
    {
        Entities entities = new Entities();

        public Counter_reading_page()
        {
            InitializeComponent();
            foreach (var couner_read in entities.Counter_reading)
                ListBox_counter_reading.Items.Add(couner_read);
        }

        private void ListBox_counter_reading_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_reading = ListBox_counter_reading.SelectedItem as Counter_reading;
            if (selected_reading != null)
            {
                TextBox_id_indication.Text = Convert.ToString(selected_reading.Id_indication);
                TextBox_id_persacc.Text = selected_reading.Id_personal_account;
                TextBox_number_counter.Text = Convert.ToString(selected_reading.Id_counter);
                DataPicker_current_indication.SelectedDate = selected_reading.Date_current_indication;
                TextBox_current_pokaz.Text = Convert.ToString(selected_reading.Current_indication);
                DataPicker_past_indication.SelectedDate = selected_reading.Date_past_indication;
                TextBox_past_pokaz.Text = Convert.ToString(selected_reading.Past_indication);
                TextBox_consumption.Text = Convert.ToString(selected_reading.Consumption);
                TextBox_value_tarif.Text = Convert.ToString(selected_reading.Value_tarif);
                TextBox_accrued.Text = Convert.ToString(selected_reading.Accrued);
                ListBox_counter_reading.Items.Refresh();
            }
        }

        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            var counter = ListBox_counter_reading.SelectedItem as Counter_reading;
            if (TextBox_id_indication.Text == "" || TextBox_number_counter.Text == "" || TextBox_id_persacc.Text == "" ||
                TextBox_current_pokaz.Text == "" || TextBox_past_pokaz.Text == "" || 
                TextBox_value_tarif.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (counter == null)
                {
                    decimal current_ind, past_ind, consumption, accrued, value;
                    current_ind = Convert.ToDecimal(TextBox_current_pokaz.Text);
                    past_ind = Convert.ToDecimal(TextBox_past_pokaz.Text);
                    value = Convert.ToDecimal(TextBox_value_tarif.Text);
                    consumption = current_ind - past_ind;
                    TextBox_consumption.Text = Convert.ToString(consumption);

                    accrued = value * consumption;
                    TextBox_accrued.Text = Convert.ToString(accrued);

                    counter = new Counter_reading();
                    entities.Counter_reading.Add(counter);

                    ListBox_counter_reading.Items.Add(counter);
                    MessageBox.Show("Запись добавлена!", "Операция выполнена", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                counter.Id_indication = Convert.ToInt32(TextBox_id_indication.Text);
                counter.Id_personal_account = TextBox_id_persacc.Text;
                counter.Id_counter = Convert.ToInt32(TextBox_number_counter.Text);
                counter.Date_current_indication = DataPicker_current_indication.SelectedDate;
                counter.Current_indication = Convert.ToInt32(TextBox_current_pokaz.Text);
                counter.Date_past_indication = DataPicker_past_indication.SelectedDate;
                counter.Past_indication = Convert.ToInt32(TextBox_past_pokaz.Text);
                counter.Consumption = Convert.ToInt32(TextBox_consumption.Text);
                counter.Value_tarif = Convert.ToDecimal(TextBox_value_tarif.Text);
                counter.Accrued = Convert.ToDecimal(TextBox_accrued.Text);
                
                entities.Counter_reading.Add(counter);
                entities.SaveChanges();

                ListBox_counter_reading.Items.Refresh();

                TextBox_id_indication.Clear();
                TextBox_id_persacc.Clear();
                TextBox_number_counter.Clear();
                DataPicker_current_indication.Text = null;
                TextBox_current_pokaz.Clear();
                DataPicker_past_indication.Text = null;
                TextBox_past_pokaz.Clear();
                TextBox_consumption.Clear();
                TextBox_value_tarif.Clear();
                TextBox_accrued.Clear();
            }
        }

        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            var rez = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.No)
                return;
            var delete_counter_reading = ListBox_counter_reading.SelectedItem as Counter_reading;
            if (delete_counter_reading != null)
            {
                entities.Counter_reading.Remove(delete_counter_reading);

                TextBox_id_indication.Clear();
                TextBox_id_persacc.Clear();
                TextBox_number_counter.Clear();
                DataPicker_current_indication.Text = null;
                TextBox_current_pokaz.Clear();
                DataPicker_past_indication.Text = null;
                TextBox_past_pokaz.Clear();
                TextBox_consumption.Clear();
                TextBox_value_tarif.Clear();
                TextBox_accrued.Clear();
                ListBox_counter_reading.Items.Refresh();
                entities.SaveChanges();
            }
        }

        private void Button_excel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel не установлен!!");
                return;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            

            xlWorkSheet.Cells[1, 1] = "Показания счётчиков";
            xlWorkSheet.Cells[2, 1] = "Номер показания";
            xlWorkSheet.Cells[2, 2] = "Номер расчётного счёта";
            xlWorkSheet.Cells[2, 3] = "Номер счётчика";
            xlWorkSheet.Cells[2, 4] = "Дата текущих показаний";
            xlWorkSheet.Cells[2, 5] = "Текущие показания";
            xlWorkSheet.Cells[2, 6] = "Дата предыдущих показаний";
            xlWorkSheet.Cells[2, 7] = "Предыдущие показания";
            xlWorkSheet.Cells[2, 8] = "Расход";
            xlWorkSheet.Cells[2, 9] = "Стоимость тарифа";
            xlWorkSheet.Cells[2, 10] = "Начислено";

            xlWorkSheet.Cells[3, 1] = TextBox_id_indication.Text;
            xlWorkSheet.Cells[3, 2] = TextBox_id_persacc.Text;
            xlWorkSheet.Cells[3, 3] = TextBox_number_counter.Text;
            xlWorkSheet.Cells[3, 4] = DataPicker_current_indication.Text;
            xlWorkSheet.Cells[3, 5] = TextBox_current_pokaz.Text;
            xlWorkSheet.Cells[3, 6] = DataPicker_past_indication.Text;
            xlWorkSheet.Cells[3, 7] = TextBox_current_pokaz.Text;
            xlWorkSheet.Cells[3, 8] = TextBox_consumption.Text;
            xlWorkSheet.Cells[3, 9] = TextBox_value_tarif.Text;
            xlWorkSheet.Cells[3, 10] = TextBox_accrued.Text;

            xlWorkBook.SaveAs("D:\\4 курс\\КУРСАЧ\\Electricity_biling_Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Файл Excel создан", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
