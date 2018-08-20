using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    // Interaction logic for RemoveDataColumnDesigner.xaml
    public partial class RemoveDataColumnDesigner
    {

        public String SelectedItem
        {
            get; set;
        }

        public RemoveDataColumnDesigner()
        {
            this.SelectedItem = "Column Name";
            InitializeComponent();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            // name object index
            switch (cb.SelectedIndex) {
                case 0:
                    ModelItem.Properties["ColumnObject"].SetValue(null);
                    ModelItem.Properties["ColumnIndex"].SetValue(null);
                    break;
                case 1:
                    ModelItem.Properties["ColumnIndex"].SetValue(null);
                    ModelItem.Properties["ColumnName"].SetValue(null);
                    break;
                case 2:
                    ModelItem.Properties["ColumnName"].SetValue(null);
                    ModelItem.Properties["ColumnObject"].SetValue(null);
                    break;
            }
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DesignerAttribute(typeof(RemoveDataColumnDesigner)));
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }


}
