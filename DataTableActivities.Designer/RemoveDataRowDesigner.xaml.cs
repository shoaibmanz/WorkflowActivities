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
    // Interaction logic for RemoveDataRowDesigner.xaml
    public partial class RemoveDataRowDesigner
    {
        public RemoveDataRowDesigner()
        {
            InitializeComponent();
            cbChoices.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ModelItem.Properties["RowIndex"].SetValue(null);            
            ModelItem.Properties["RowObject"].SetValue(null);
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RemoveDataRow), new DesignerAttribute(typeof(RemoveDataRowDesigner)));
            builder.AddCustomAttributes(typeof(RemoveDataRow), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }
}
