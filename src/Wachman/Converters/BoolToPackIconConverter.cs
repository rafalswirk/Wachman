using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wachman.Converters
{
    internal class BoolToPackIconConverter : IValueConverter
    {
        public PackIconKind IsTrueIcon { get; set; }
        public PackIconKind IsFalseIcon { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isActive = (bool)value;
            if (isActive)
            {
                return IsTrueIcon;
            }
            return IsFalseIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
