using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace DataTableActivities
{

    public class RemoveDataRow : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [OverloadGroup("Row")]
        [RequiredArgument]
        [Category("Input")]
        public InArgument<DataRow> RowObject
        {
            get;
            set;
        }

        [OverloadGroup("RowIndex")]
        [RequiredArgument]
        [Category("Input")]
        public InArgument<int> RowIndex
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);
            DataRow dataRow = this.RowObject.Get(context);
            if (dataRow != null)
            {
                dataTable.Rows.Remove(dataRow);
            }
            else
            {
                dataTable.Rows.RemoveAt(this.RowIndex.Get(context));
            }
            this.DataTable.Set(context, dataTable);
        }
    }
}
