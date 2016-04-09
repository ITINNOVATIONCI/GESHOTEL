using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GESHOTEL.AmenitiesModules.ViewModels
{
    public class EditorTemplateRule
    {
        private string propertyName;
        private DataTemplate dataTemplate;

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the data template.
        /// </summary>
        /// <value>The data template.</value>
        public DataTemplate DataTemplate
        {
            get
            {
                return this.dataTemplate;
            }
            set
            {
                this.dataTemplate = value;
            }
        }
    }
}
