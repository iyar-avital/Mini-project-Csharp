using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;


namespace PLWPF
{
    class ConvertOptionToYesOrNot : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BE.Option option = (BE.Option)value;
            if (option == BE.Option.Necessary)
                return BE.YesOrNot.there_is;
            else
                return BE.YesOrNot.there_isnt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BE.YesOrNot yesOrNot = (BE.YesOrNot)value;
            if (yesOrNot == BE.YesOrNot.there_is)
                return BE.Option.Necessary;
            else
                return BE.Option.uninterested;    
        }
    }
}
