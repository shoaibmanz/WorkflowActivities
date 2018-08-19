using System.Activities;
using System.ComponentModel;
using System.Data;

namespace DataTableActivities
{

    public class ClearDataTable : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);

            dataTable.Clear();
        }
    }
}
