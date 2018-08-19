using System.Activities;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public class RemoveDataColumn : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [OverloadGroup("ColumnIndex")]
        [RequiredArgument]
        [Category("Input")]
        public InArgument<int> ColumnIndex
        {
            get;
            set;
        }

        [OverloadGroup("ColumnName"), RequiredArgument, Category("Input")]
        public InArgument<string> ColumnName
        {
            get;
            set;
        }

        [Category("Input")]
        public InArgument<DataColumn> ColumnObject
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            
            DataTable dataTable = this.DataTable.Get(context);
            string value = this.ColumnName.Get(context);
            DataColumn dataColumn = this.ColumnObject.Get(context);
            if (dataColumn != null)
            {
                dataTable.Columns.Remove(dataColumn);
            }
            else if (string.IsNullOrWhiteSpace(value))
            {
                dataTable.Columns.RemoveAt(this.ColumnIndex.Get(context));
            }
            else
            {
                dataTable.Columns.Remove(this.ColumnName.Get(context));
            }
            this.DataTable.Set(context, dataTable);
        }
    }
}
