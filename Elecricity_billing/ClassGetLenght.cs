using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Elecricity_billing.functions
{
    public static class TextBoxGet
    {
        public static int GetLenght(TextBox box = null, PasswordBox password = null)
        {
            return box != null ? box.Text.Trim().Length : password.Password.Trim().Length;
        }

        public static string GetText(TextBox box = null, PasswordBox password = null)
        {
            return box != null ? box.Text.Trim() : password.Password.Trim();
        }
    }
}
