using System;
using System.Collections.Generic;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace DataTableActivities
{

    public class AddDataRow : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [RequiredArgument]
        [Category("Input")]
        [OverloadGroup("DataRow")]
        public InArgument<DataRow> DataRowObject
        {
            get;
            set;
        }

        [OverloadGroup("Row")]
        [RequiredArgument, Category("Input")]
        public InArgument<object[]> ArrayRow
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);

            if (this.DataTable == null)
                return;

            if (this.DataRowObject.Get(context) != null)
            {
                dataTable.Rows.Add(this.DataRowObject.Get(context));
            }
            else
            {
                dataTable.Rows.Add(this.ArrayRow.Get(context));
            }

            this.DataTable.Set(context, dataTable);
        }
    }
}
