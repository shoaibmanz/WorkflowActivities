using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataTableActivities.Designer
{
    // Interaction logic for AddDataRowDesigner.xaml
    public partial class AddDataRowDesigner
    {
        public AddDataRowDesigner()
        {
            InitializeComponent();
        }


        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(AddDataRow), new DesignerAttribute(typeof(AddDataRowDesigner)));
            builder.AddCustomAttributes(typeof(AddDataRow), new DescriptionAttribute("Removes a Column from a Data Table"));
        }

        private void cbChoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((string)ModelItem.Properties["Selected"].ComputedValue == "Data Row")               
                ModelItem.Properties["ArrayRow"].SetValue(null);
            else
                ModelItem.Properties["DataRowObject"].SetValue(null);
        }
    }
}
