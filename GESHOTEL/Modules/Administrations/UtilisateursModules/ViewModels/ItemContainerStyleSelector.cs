using GESHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GESHOTEL.UtilisateursModules.ViewModels
{
    public class ItemContainerStyleSelector : StyleSelector
    {
        private Style menuStyle;
        private Style sousMenuStyle;
        private Style actionStyle;
        public override Style SelectStyle(object item, DependencyObject container)
        {
            Permissions per = (Permissions)item;

            if (per.Type == "M")
                return this.menuStyle;
            else if (per.Type == "SM")
                return this.sousMenuStyle;
            else if (per.Type == "AC")
                return this.actionStyle;
            return null;
        }
        public Style MenuStyle
        {
            get
            {
                return this.menuStyle;
            }
            set
            {
                this.menuStyle = value;
            }
        }
        public Style SousMenuStyle
        {
            get
            {
                return this.sousMenuStyle;
            }
            set
            {
                this.sousMenuStyle = value;
            }
        }
        public Style ActionStyle
        {
            get
            {
                return this.actionStyle;
            }
            set
            {
                this.actionStyle = value;
            }
        }
    }
}
