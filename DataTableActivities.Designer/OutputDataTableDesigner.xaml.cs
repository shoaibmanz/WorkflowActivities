using System;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataTableActivities.Designer
{
    // Interaction logic for OutputDataTableDesigner.xaml
    public partial class OutputDataTableDesigner
    {
        public OutputDataTableDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(OutputDataTable), new DesignerAttribute(typeof(OutputDataTableDesigner)));
            builder.AddCustomAttributes(typeof(OutputDataTable), new DescriptionAttribute("Converts Data Table to a readable String"));
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 1)
            {
                tb.Text = tb.Text.Substring(0, 1);
            }
        }
    }
}
