using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace DataTableActivities.Designer
{
    // Interaction logic for ActivityDesigner1.xaml
    public partial class GetRowItemDesigner
    {
        public GetRowItemDesigner()
        {
            InitializeComponent();

        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(GetRowItem), new DesignerAttribute(typeof(GetRowItemDesigner)));
            builder.AddCustomAttributes(typeof(GetRowItem), new DescriptionAttribute("Gets row item"));
        }
    }
}
