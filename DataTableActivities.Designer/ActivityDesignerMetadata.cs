using System.Activities.Presentation.Metadata;

namespace DataTableActivities.Designer
{
    public sealed class Metadata : IRegisterMetadata

    {

        public void Register()
        {
            RegisterAll();
        }

        public static void RegisterAll()
        {

            var builder = new AttributeTableBuilder();

            AddDataColumnDesigner.RegisterMetadata(builder);
            AddDataRowDesigner.RegisterMetadata(builder);
            OutputDataTableDesigner.RegisterMetadata(builder);
            RemoveDataColumnDesigner.RegisterMetadata(builder);
            RemoveDataRowDesigner.RegisterMetadata(builder);
            ClearDataTableDesigner.RegisterMetadata(builder);
            //BuildDataTableDesigner.RegisterMetadata(builder);
            RemoveDuplicateValuesDesigner.RegisterMetadata(builder);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
