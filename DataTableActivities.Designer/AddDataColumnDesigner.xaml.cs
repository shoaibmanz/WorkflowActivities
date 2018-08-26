using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace DataTableActivities.Designer
{
    // Interaction logic for AddDataColumnDesigner.xaml
    public partial class AddDataColumnDesigner
    {
        public AddDataColumnDesigner()
        {
            InitializeComponent();
        }

        private void ConfigureColumn_Click(object sender, RoutedEventArgs e)
        {
            DataColumn dataColumn = (DataColumn)ModelItem.Properties["dataColumn"].ComputedValue;
            var Activity = (ModelItem.GetCurrentValue();
            var NewWindow = new NewColumnDialog(ModelItem, (DataColumn)ModelItem.Properties["dataColumn"].ComputedValue);

            NewWindow.ShowDialog();

            if (NewWindow.SaveChanges)
            {
                this.ModelItem.Properties["dataColumn"].SetValue(dataColumn);
            }

        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(AddDataColumn), new DesignerAttribute(typeof(AddDataColumnDesigner)));
            builder.AddCustomAttributes(typeof(AddDataColumn), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }
}
