using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    // Interaction logic for BuildDataTableDesigner.xaml
    public partial class BuildDataTableDesigner
    {

        public BuildDataTableDesigner()
        {
            this.InitializeComponent();
        }

        private void DatatableButton_Click(object sender, RoutedEventArgs e)
        {
            StringReader reader = new StringReader(base.ModelItem.Properties["TableInfo"].ComputedValue as string);
            DataTable expr_2A = new DataTable();
            expr_2A.ReadXml(reader);
            BuildDataTableDialog buildDataTableDialog = new BuildDataTableDialog(expr_2A, base.ModelItem);
            buildDataTableDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            buildDataTableDialog.ShowDialog();
            if (buildDataTableDialog.SaveTable)
            {
                StringWriter stringWriter = new StringWriter();
                buildDataTableDialog.DataTable.WriteXml(stringWriter, XmlWriteMode.WriteSchema);
                base.ModelItem.Properties["TableInfo"].SetValue(stringWriter.ToString());
            }
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(BuildDataTable), new DesignerAttribute(typeof(BuildDataTableDesigner)));
            builder.AddCustomAttributes(typeof(BuildDataTable), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }
}
