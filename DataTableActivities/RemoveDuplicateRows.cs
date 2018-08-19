using System;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace DataTableActivities
{

    public class RemoveDuplicateRows : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        public InArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [Category("Output"), DisplayName("DataTable")]
        public OutArgument<DataTable> OutputDataTable
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            
            if (this.DataTable.Get(context) == null)
            {
                throw new ArgumentException("DataTable");
            }

            DataTable table = this.DataTable.Get(context);

            if (this.OutputDataTable != null)
            {
                this.OutputDataTable.Set(context, table.DefaultView.ToTable(true, new string[0]));
            }
        }
    }
}
