using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GESHOTEL.Models;

namespace GESHOTEL.UtilisateursModules.ViewModels
{
    public class HierarchyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //// We are binding an item
            //Permissions item = value as Permissions;
            //if (item != null)
            //{
            //    return item.Where(i => i.ParentId == item.Id);
            //}
            //// We are binding the treeview
            //DataItemCollection items = value as DataItemCollection;
            //if (items != null)
            //{
            //    return items.Where(i => i.ParentId == 0);
            //}
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
