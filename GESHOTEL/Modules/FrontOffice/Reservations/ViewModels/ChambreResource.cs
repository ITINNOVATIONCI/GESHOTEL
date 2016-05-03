using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace GESHOTEL.ReservationsModules.ViewModel
{
    public class ChambreResource : Resource
    {
        public ChambreResource(string name, string type)
            :base(name, type)
        {
            //this.ImageFileName = imageFileName;
        }

    }

    public class TypeChambreResource : Resource
    {
        public TypeChambreResource(string name, string type)
            : base(name, type)
        {
            //this.ImageFileName = imageFileName;
        }

    }
}
