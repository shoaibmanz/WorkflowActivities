using System;
using System.Activities;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public class AddDataColumn : CodeActivity
    {
        public DataColumn dataColumn;

        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        public AddDataColumn()
        {
            this.dataColumn = new DataColumn();

            this.dataColumn.DataType = typeof(String);

            this.dataColumn.MaxLength = -1;
            this.dataColumn.AllowDBNull = true;
            this.dataColumn.Unique = false;
            this.dataColumn.AutoIncrement = false;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);

            dataTable.Columns.Add(dataColumn);

            this.DataTable.Set(context, dataTable);
        }
    }
}
