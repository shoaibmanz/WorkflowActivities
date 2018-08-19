using System;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
    }
}
