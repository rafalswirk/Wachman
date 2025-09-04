using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FocusForge.UI.Dialogs
{
    public class WindowsDialog : IDialog
    {
        public void Show(DialogData data)
        {
            MessageBox.Show(data.Message, "FocusForge");
        }
    }
}
