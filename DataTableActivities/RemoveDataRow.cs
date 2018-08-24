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
        [Browsable(false)]
        public int Selected
        {
            get;
            set;
        }

        [Browsable(false)]
        public string[] Options
        {
            get;
            set;
        } = { "Data Row", "Row Index" };



        public RemoveDataRow()
        {
            this.Selected = 0;
        }


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

            switch (this.Selected)
            {
                case 0:
                    dataTable.Rows.Remove(this.RowObject.Get(context));
                    break;
                case 1:
                    dataTable.Rows.RemoveAt(this.RowIndex.Get(context));
                    break;
            }

            this.DataTable.Set(context, dataTable);
        }
    }
}
