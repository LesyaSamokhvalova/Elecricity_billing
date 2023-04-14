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

namespace Elecricity_billing
{
    /// <summary>
    /// Логика взаимодействия для Tariff_page.xaml
    /// </summary>
    public partial class Tariff_page : Page
    {
        Entities entities = new Entities();
        public Tariff_page()
        {
            InitializeComponent();
            foreach(var tarif in entities.Tarif)
                ListBox_Tariff.Items.Add(tarif);

        }

        private void ListBox_Tariff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_tarif = ListBox_Tariff.SelectedItem as Tarif;
            if(selected_tarif != null)
            {
                TextBox_id_tarif.Text = Convert.ToString(selected_tarif.Id_tarif);
                TextBox_name.Text = selected_tarif.Name;
                TextBox_type_tarif.Text = selected_tarif.Type_tarif;
                TextBox_value_tarif.Text = Convert.ToString(selected_tarif.Value_tarif);
                TextBlock_about_tariff.Text = selected_tarif.About_tarif;
                ListBox_Tariff.Items.Refresh();
            }
            else
            {
                TextBox_id_tarif.Text = " ";
                TextBox_name.Text = " ";
                TextBox_type_tarif.Text = " ";
                TextBox_value_tarif.Text = " ";
                ListBox_Tariff.Items.Refresh();
            }
        }
    }
}
