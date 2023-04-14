using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elecricity_billing
{
    public partial class Reading_indication
    {
        public override string ToString()
        {
            return Id_payment + ". " + Id_personal_account + " " +
                Opening_balance + " " + Accrued + " " + Total_penalties + " " +
                Paid + " " + Closing_balance;
        }
    }

    public partial class Perconal_account
    {
        public override string ToString()
        {
            return Id_personal_account + ". " +
                Payer + " " + Number_lot + " " +
                Land_area + " " + Id_counter;
        }
    }

    public partial class Counter
    {
        public override string ToString()
        {
            return Id_counter + ". " +
                Mark + " " + Serial_number + " " +
                Seal_number + " " + Id_tarif;
        }
    }

    public partial class Tarif
    {
        public override string ToString()
        {
            return Id_tarif + ". " + Name + " " +
                Type_tarif + " " + Value_tarif;
        }
    }
    public partial class Counter_reading
    {
        public override string ToString()
        {
            return Id_indication + ". " + Id_personal_account + " " +
                Id_counter + " " + Date_current_indication + " " +
                Current_indication + " " + Date_past_indication + " " +
                Past_indication + " " + Consumption + " " + Value_tarif +
                Accrued;
        }
    }
}
