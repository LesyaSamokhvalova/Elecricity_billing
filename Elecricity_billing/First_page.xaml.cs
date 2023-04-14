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
    /// Логика взаимодействия для First_page.xaml
    /// </summary>
    public partial class First_page : Page
    {
        Entities entities = new Entities();

        public First_page()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Clear();
            var query_1 = from grid_reading in entities.Reading_indication
                          select new
                          {
                              Id_payment = grid_reading.Id_payment,
                              Id_personal_account = grid_reading.Id_personal_account,
                              Opening_balance = grid_reading.Opening_balance,
                              Accrued = grid_reading.Accrued,
                              Total_penalties = grid_reading.Total_penalties,
                              Paid = grid_reading.Paid,
                              Closing_balance = grid_reading.Closing_balance
                          };
            dataGrid.ItemsSource = query_1.ToList();

        }
    }
}
