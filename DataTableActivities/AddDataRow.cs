using System;
using System.Collections.Generic;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace DataTableActivities
{

    public class AddDataRow : CodeActivity
    {
        [Browsable(false)]
        public string Selected
        {
            get;
            set;
        }

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

        public AddDataRow()
        {
            this.Selected = "Data Row";
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);

            if (this.DataTable == null)
                return;

            if (Selected == "Data Row")
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
