using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public class AddDataColumn : CodeActivity
    {
        [Browsable(false)]
        public DataColumn dataColumn
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


        public AddDataColumn()
        {
            this.dataColumn = new DataColumn();
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);

            dataTable.Columns.Add(dataColumn);

            this.DataTable.Set(context, dataTable);
        }
    }
}
