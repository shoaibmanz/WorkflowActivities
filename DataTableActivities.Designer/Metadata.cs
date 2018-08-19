using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
