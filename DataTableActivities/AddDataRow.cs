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
        } = { "Data Row", "Array Row" };



        public AddDataRow()
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

            switch (Selected)
            {
                case 0:
                    dataTable.Rows.Add(this.DataRowObject.Get(context));
                    break;

                case 1:
                    dataTable.Rows.Add(this.ArrayRow.Get(context));
                    break;
            }

            this.DataTable.Set(context, dataTable);
        }
    }
}
