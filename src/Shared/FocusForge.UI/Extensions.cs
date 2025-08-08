using FocusForge.UI.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace FocusForge.UI
{
    public static class Extensions
    {
        public static void AddSharedUI(this IServiceCollection services)
        {
            services.AddSingleton<IDialog, WindowsDialog>();
        }
    }
}
