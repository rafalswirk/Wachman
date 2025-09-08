using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusForge.Shared.UI.Dialogs
{
    public interface IDialog
    {
        void Show(DialogData data);
    }
}
